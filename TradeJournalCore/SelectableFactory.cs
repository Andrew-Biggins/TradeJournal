using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public enum TradeStatus
    {
        Open,
        Closed,
        Both
    }

    internal static class SelectableFactory
    {
        internal static SelectableCollection<IMarket> GetDefaultMarkets()
        {
            var collection = new SelectableCollection<IMarket>();
            
            try
            {
                var connection = new SqlConnection(DataConnection.Builder.ConnectionString);
                connection.Open();

                const string sql = "SELECT * From Market;";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var assetClass = (AssetClass) Enum.Parse(typeof(AssetClass), reader.GetFieldValue<string>(2));
                            var pipDivisor = (PipDivisor) Enum.Parse(typeof(PipDivisor), reader.GetFieldValue<string>(3));
                            collection.AddSelectable(new Market(reader.GetFieldValue<string>(1), assetClass, pipDivisor) { IsSelected = true });
                        }
                    }
                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
           
            return collection;
        }

        internal static SelectableCollection<ISelectable> GetDefaultStrategies()
        {
            var collection = new SelectableCollection<ISelectable>();

            try
            {
                var connection = new SqlConnection(DataConnection.Builder.ConnectionString);
                connection.Open();

                const string sql = "SELECT * From Strategy;";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            collection.AddSelectable(new Strategy(reader.GetFieldValue<string>(1)) { IsSelected = true });
                        }
                    }
                }

                connection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }


            return collection;
        }

        internal static SelectableCollection<ISelectable> GetAssetTypes()
        {
            var list = new SelectableCollection<ISelectable>();

            var assetClasses = (AssetClass[])Enum.GetValues(typeof(AssetClass));

            foreach (var assetClass in assetClasses)
            {
                list.AddSelectable(new AssetType(assetClass) { IsSelected = true });
            }

            return list;
        }

        internal static SelectableCollection<ISelectable> GetDays()
        {
            var list = new SelectableCollection<ISelectable>();

            var days = (DayOfWeek[])Enum.GetValues(typeof(DayOfWeek));

            foreach (var day in days)
            {
                list.AddSelectable(new Day(day) { IsSelected = true });
            }

            return list;
        }

        internal static IReadOnlyList<T> GetEnumList<T>()
        {
            return ((T[]) Enum.GetValues(typeof(T))).ToList();
        }
    }
}
