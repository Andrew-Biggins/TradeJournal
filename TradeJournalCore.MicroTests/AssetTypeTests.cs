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

        [Gwt("Given an asset type",
            "when the is selected flag is updated",
            "property changed is raised for is selected")]
        public void T2()
        {
            // Arrange
            var assetType = new AssetType(AssetClass.Indices);
            var catcher = Catcher.For(assetType);

            // Act 
            assetType.IsSelected = true;

            // Assert
            catcher.CaughtPropertyChanged(assetType, nameof(assetType.IsSelected));
        }
    }
}
