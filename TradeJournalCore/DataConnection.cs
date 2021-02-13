using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using Common.Optional;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    internal static class DataConnection
    {
        internal static IList<ITrade> GetAllTrades()
        {
            var trades = new List<ITrade>();

            try
            {
                using var connection = new SqlConnection(Builder.ConnectionString);
                connection.Open();

                const string sql = "SELECT * From Trade INNER JOIN Market ON Trade.MarketId=Market.Id" +
                                   " INNER JOIN Strategy ON Trade.StrategyId=Strategy.Id;";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    var results = cmd.ExecuteReader();

                    if (results.HasRows)
                    {
                        while (results.Read())
                        {
                            var assetClass = (AssetClass)Enum.Parse(typeof(AssetClass), results.GetFieldValue<string>(17));
                            var pipDivisor = (PipDivisor)Enum.Parse(typeof(PipDivisor), results.GetFieldValue<string>(18));
                            var market = new Market(results.GetFieldValue<string>(16), assetClass, pipDivisor);
                            var strategy = new Strategy(results.GetFieldValue<string>(20));
                            var entry = results.GetFieldValue<double>(3);
                            var stop = results.GetFieldValue<double>(4);
                            var target = results.GetFieldValue<double>(5);
                            var openLevel = results.GetFieldValue<double>(6);
                            var openDateTime = results.GetFieldValue<DateTime>(7);
                            var openSize = results.GetFieldValue<double>(8);
                            var id = results.GetFieldValue<int>(0);
                            var entryOrderType = (EntryOrderType)Enum.Parse(typeof(EntryOrderType), results.GetFieldValue<string>(14));

                            var close = Option.None<Execution>();

                            if (!results.IsDBNull(9))
                            {
                                var closeLevel = results.GetFieldValue<double>(9);
                                var closeDateTime = results.GetFieldValue<DateTime>(10);
                                var closeSize = results.GetFieldValue<double>(11);
                                close = Option.Some(new Execution(closeLevel, closeDateTime, closeSize));
                            }

                            var mae = Option.None<double>();

                            if (!results.IsDBNull(12))
                            {
                                mae = Option.Some(results.GetFieldValue<double>(12));
                            }

                            var mfe = Option.None<double>();

                            if (!results.IsDBNull(13))
                            {
                                mfe = Option.Some(results.GetFieldValue<double>(13));
                            }

                            trades.Add(new Trade(market,
                                strategy, new Levels(entry, stop, target),
                                new Execution(openLevel, openDateTime, openSize), close,
                                (mae, mfe), entryOrderType){Id = id});
                        }
                    }
                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return trades;
        }

        internal static void AddMarket(Market market)
        {
            try
            {
                var connection = new SqlConnection(Builder.ConnectionString);
                connection.Open();

                const string sql = "INSERT INTO Market (Name, AssetClass, PipDivisor) " +
                                   "VALUES (@name, @assetClass, @pipDivisor)";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", market.Name);
                    cmd.Parameters.AddWithValue("@assetClass", market.AssetClass.ToString());
                    cmd.Parameters.AddWithValue("@pipDivisor", market.PipDivisor.ToString());
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        internal static void AddStrategy(Strategy strategy)
        {
            try
            {
                var connection = new SqlConnection(Builder.ConnectionString);
                connection.Open();

                const string sql = "INSERT INTO Strategy (Name) VALUES (@name)";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", strategy.Name);
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        internal static void AddTrade(ITrade trade)
        {
            var marketId = GetId("SELECT Id FROM Market WHERE Name = @name", trade.Market.Name);
            var strategyId = GetId("SELECT Id FROM Strategy WHERE Name = @name", trade.Strategy.Name);

            try
            {
                var connection = new SqlConnection(Builder.ConnectionString);
                connection.Open();

                const string sql =
                    "INSERT INTO Trade (MarketId, StrategyId, Entry, Stop, Target, OpenLevel, OpenDateTime, OpenSize, " +
                    "CloseLevel, CloseDateTime, CloseSize, MaxAdverseExcursion, MaxFavourableExcursion, EntryOrderType) " +
                    "OUTPUT INSERTED.Id VALUES (@marketId, @strategyId, @entry, @stop, @target, @openLevel," +
                    "@openDateTime, @openSize, @closeLevel, @closeDateTime, @closeSize, @mae, @mfe, @entryOrderType)";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@marketId", marketId);
                    cmd.Parameters.AddWithValue("@strategyId", strategyId);
                    cmd.Parameters.AddWithValue("@entry", trade.Levels.Entry);
                    cmd.Parameters.AddWithValue("@stop", trade.Levels.Stop);
                    cmd.Parameters.AddWithValue("@target", trade.Levels.Target);
                    cmd.Parameters.AddWithValue("@openLevel", trade.Open.Level);
                    cmd.Parameters.AddWithValue("@openDateTime", trade.Open.DateTime);
                    cmd.Parameters.AddWithValue("@openSize", trade.Open.Size);

                    object closeLevel = DBNull.Value;
                    object closeDateTime = DBNull.Value;
                    object closeSize = DBNull.Value;
                    trade.Close.IfExistsThen(x =>
                    {
                        closeLevel = x.Level;
                        closeDateTime = x.DateTime;
                        closeSize = x.Size;
                    });

                    cmd.Parameters.AddWithValue("@closeLevel", closeLevel);
                    cmd.Parameters.AddWithValue("@closeDateTime", closeDateTime);
                    cmd.Parameters.AddWithValue("@closeSize", closeSize);

                    object mae = DBNull.Value;
                    trade.MaxAdverseExcursion.IfExistsThen(x => mae = x);
                    cmd.Parameters.AddWithValue("@mae", mae);

                    object mfe = DBNull.Value;
                    trade.MaxAdverseExcursion.IfExistsThen(x => mfe = x);
                    cmd.Parameters.AddWithValue("@mfe", mfe);

                    cmd.Parameters.AddWithValue("@entryOrderType", trade.EntryOrderType.ToString());

                    trade.Id = (int) cmd.ExecuteScalar();
                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        internal static void RemoveTrade(int id)
        {
            try
            {
                var connection = new SqlConnection(Builder.ConnectionString);
                connection.Open();

                const string sql = "DELETE FROM Trade WHERE Id=@idNumber";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@IDNumber", id);
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        internal static void ClearTrades()
        {
            try
            {
                var connection = new SqlConnection(Builder.ConnectionString);
                connection.Open();

                const string sql = "TRUNCATE TABLE Trade";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        internal static IList<ITrade> GetOpenTradesByAssetClass(AssetClass ac)
        {
            var trades = new List<ITrade>();

            try
            {
                using var connection = new SqlConnection(Builder.ConnectionString);
                connection.Open();

                const string sql = "SELECT * From Trade INNER JOIN Market ON Trade.MarketId=Market.Id" +
                                   " INNER JOIN Strategy ON Trade.StrategyId=Strategy.Id WHERE CloseLevel IS NULL AND Market.AssetClass = @assetClass;";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@assetClass", ac.ToString());
                    var results = cmd.ExecuteReader();

                    if (results.HasRows)
                    {
                        while (results.Read())
                        {
                            var assetClass = (AssetClass)Enum.Parse(typeof(AssetClass), results.GetFieldValue<string>(17));
                            var pipDivisor = (PipDivisor)Enum.Parse(typeof(PipDivisor), results.GetFieldValue<string>(18));
                            var market = new Market(results.GetFieldValue<string>(16), assetClass, pipDivisor);
                            var strategy = new Strategy(results.GetFieldValue<string>(20));
                            var entry = results.GetFieldValue<double>(3);
                            var stop = results.GetFieldValue<double>(4);
                            var target = results.GetFieldValue<double>(5);
                            var openLevel = results.GetFieldValue<double>(6);
                            var openDateTime = results.GetFieldValue<DateTime>(7);
                            var openSize = results.GetFieldValue<double>(8);
                            var id = results.GetFieldValue<int>(0);
                            var entryOrderType = (EntryOrderType)Enum.Parse(typeof(EntryOrderType), results.GetFieldValue<string>(14));

                            var close = Option.None<Execution>();

                            if (!results.IsDBNull(9))
                            {
                                var closeLevel = results.GetFieldValue<double>(9);
                                var closeDateTime = results.GetFieldValue<DateTime>(10);
                                var closeSize = results.GetFieldValue<double>(11);
                                close = Option.Some(new Execution(closeLevel, closeDateTime, closeSize));
                            }

                            var mae = Option.None<double>();

                            if (!results.IsDBNull(12))
                            {
                                mae = Option.Some(results.GetFieldValue<double>(12));
                            }

                            var mfe = Option.None<double>();

                            if (!results.IsDBNull(13))
                            {
                                mfe = Option.Some(results.GetFieldValue<double>(13));
                            }

                            trades.Add(new Trade(market,
                                strategy, new Levels(entry, stop, target),
                                new Execution(openLevel, openDateTime, openSize), close,
                                (mae, mfe), entryOrderType)
                            { Id = id });
                        }
                    }
                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return trades;
        }


        internal static SqlConnectionStringBuilder Builder
        {
            get
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = "(localdb)\\MSSQLLocalDB",
                    InitialCatalog = "TradeJournalDatabase",
                    IntegratedSecurity = true,
                    PersistSecurityInfo = true
                };

                return builder;
            }
        }

        private static int GetId(string sql, string name)
        {
            try
            {
                var connection = new SqlConnection(Builder.ConnectionString);
                connection.Open();

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return reader.GetFieldValue<int>(0);
                        }
                    }
                }

                connection.Close();
            }

            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return 0;
        }
    }
}
