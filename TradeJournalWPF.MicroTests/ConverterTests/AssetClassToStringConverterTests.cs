using Common.MicroTests;
using TradeJournalCore;
using TradeJournalWPF.Converters;
using Xunit;
using static TradeJournalWPF.MicroTests.Shared;

namespace TradeJournalWPF.MicroTests.ConverterTests
{
    public sealed class AssetClassToStringConverterTests
    {
        [GwtTheory("Given an asset class",
            "when converted",
            "the correct string is returned")]
        [InlineData(AssetClass.Commodities, "Commodities")]
        [InlineData(AssetClass.Crypto, "Crypto")]
        [InlineData(AssetClass.Currencies, "Currencies")]
        [InlineData(AssetClass.Indices, "Indices")]
        [InlineData(AssetClass.Shares, "Shares")]
        public void T0(AssetClass assetClass, string expected)
        {
            // Arrange
            var testConverter = new AssetClassToStringConverter(); 

            // Act
            var actual = testConverter.Convert(assetClass, typeof(object), null!, InvariantCulture);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
