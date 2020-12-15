using Common.MicroTests;
using Xunit;

namespace TradeJournalCore.MicroTests
{
    public sealed class AssetTypeTests
    {
        [Gwt("Given an asset type",
            "when the name is read",
            "it is the same as the one given at construction")]
        public void T0()
        {
            // Arrange
            var assetType = new AssetType(AssetClass.Indices);

            // Act 
            var actual = assetType.Name;

            // Assert
            Assert.Equal("Indices", actual);
        }

        [Gwt("Given an asset type",
            "when the is selected flag is read",
            "it is false by default")]
        public void T1()
        {
            // Arrange
            var assetType = new AssetType(AssetClass.Indices);

            // Act 
            var actual = assetType.IsSelected;

            // Assert
            Assert.False(actual);
        }

        [Gwt("Given an asset type with a false is selected flag",
            "when the is selected flag is set to false again",
            "the is selected flag is true")]
        public void T2()
        {
            // Arrange
            var assetType = new AssetType(AssetClass.Indices);

            // Act 
            assetType.IsSelected = false;

            // Assert
            Assert.True(assetType.IsSelected);
        }

        [Gwt("Given an asset type with a true is selected flag",
            "when the is selected flag is set to false ",
            "the is selected flag is false")]
        public void T3()
        {
            // Arrange
            var assetType = new AssetType(AssetClass.Indices);
            assetType.IsSelected = true;

            // Act 
            assetType.IsSelected = false;

            // Assert
            Assert.False(assetType.IsSelected);
        }
    }
}
