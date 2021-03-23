using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using Common.Optional;
using TradeJournalCore.Interfaces;
using static TradeJournalCore.DateTimeHelper;

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
                            trades.Add(ReadTradeIn(results));
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
                                   "OUTPUT INSERTED.Id " +
                                   "VALUES (@name, @assetClass, @pipDivisor)";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", market.Name);
                    cmd.Parameters.AddWithValue("@assetClass", market.AssetClass.ToString());
                    cmd.Parameters.AddWithValue("@pipDivisor", market.PipDivisor.ToString());
                    market.Id = (int)cmd.ExecuteScalar();
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

                const string sql = "INSERT INTO Strategy (Name) OUTPUT INSERTED.Id VALUES (@name)";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", strategy.Name);
                    strategy.Id = (int)cmd.ExecuteScalar();
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
                    "CloseLevel, CloseDateTime, CloseSize, High, Low, EntryOrderType) " +
                    "OUTPUT INSERTED.Id VALUES (@marketId, @strategyId, @entry, @stop, @target, @openLevel," +
                    "@openDateTime, @openSize, @closeLevel, @closeDateTime, @closeSize, @high, @low, @entryOrderType)";

                var openDateTime = CombineDateTime(trade.Open.Date, trade.Open.Time);

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@marketId", marketId);
                    cmd.Parameters.AddWithValue("@strategyId", strategyId);
                    cmd.Parameters.AddWithValue("@entry", trade.Levels.Entry);
                    cmd.Parameters.AddWithValue("@stop", trade.Levels.Stop);
                    cmd.Parameters.AddWithValue("@target", trade.Levels.Target);
                    cmd.Parameters.AddWithValue("@openLevel", trade.Open.Level);
                    cmd.Parameters.AddWithValue("@openDateTime", openDateTime);
                    cmd.Parameters.AddWithValue("@openSize", trade.Open.Size);

                    object closeLevel = DBNull.Value;
                    object closeDateTime = DBNull.Value;
                    object closeSize = DBNull.Value;
                    trade.Close.IfExistsThen(x =>
                    {
                        closeLevel = x.Level;
                        closeDateTime = CombineDateTime(x.Date, x.Time);
                        closeSize = x.Size;
                    });

                    cmd.Parameters.AddWithValue("@closeLevel", closeLevel);
                    cmd.Parameters.AddWithValue("@closeDateTime", closeDateTime);
                    cmd.Parameters.AddWithValue("@closeSize", closeSize);

                    object high = DBNull.Value;
                    trade.High.IfExistsThen(x => high = x);
                    cmd.Parameters.AddWithValue("@high", high);

                    object low = DBNull.Value;
                    trade.Low.IfExistsThen(x => low = x);
                    cmd.Parameters.AddWithValue("@low", low);

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
                            
                            trades.Add(ReadTradeIn(results));
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
                    InitialCatalog = "TradeJournalDatabaseP",
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

        private static ITrade ReadTradeIn(DbDataReader data)
        {
            var assetClass = (AssetClass)Enum.Parse(typeof(AssetClass), data.GetFieldValue<string>(17));
            var pipDivisor = (PipDivisor)Enum.Parse(typeof(PipDivisor), data.GetFieldValue<string>(18));
            var market = new Market(data.GetFieldValue<string>(16), assetClass, pipDivisor) {Id = data.GetFieldValue<int>(15) };
            var strategy = new Strategy(data.GetFieldValue<string>(20)) {Id = data.GetFieldValue<int>(19) };
            var entry = data.GetFieldValue<double>(3);
            var stop = data.GetFieldValue<double>(4);
            var target = data.GetFieldValue<double>(5);
            var openLevel = data.GetFieldValue<double>(6);
            var openDateTime = data.GetFieldValue<DateTime>(7);
            var openSize = data.GetFieldValue<double>(8);
            var id = data.GetFieldValue<int>(0);
            var entryOrderType = (EntryOrderType)Enum.Parse(typeof(EntryOrderType), data.GetFieldValue<string>(14));

            var close = Option.None<Execution>();

            if (!data.IsDBNull(9))
            {
                var closeLevel = data.GetFieldValue<double>(9);
                var closeDateTime = data.GetFieldValue<DateTime>(10);
                var closeSize = data.GetFieldValue<double>(11);
                close = Option.Some(new Execution(closeLevel, closeDateTime, closeSize));
            }

            var high = Option.None<double>();

            if (!data.IsDBNull(12))
            {
                high = Option.Some(data.GetFieldValue<double>(12));
            }

            var low = Option.None<double>();

            if (!data.IsDBNull(13))
            {
                low = Option.Some(data.GetFieldValue<double>(13));
            }

            return new Trade(market, strategy, new Levels(entry, stop, target),
                    new Execution(openLevel, openDateTime, openSize), close, 
                    (high, low), entryOrderType) { Id = id };
        }
    }
}
