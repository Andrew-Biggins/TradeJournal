CREATE TABLE [dbo].[Trade]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MarketId] INT NOT NULL, 
    [StrategyId] INT NOT NULL, 
    [Entry] FLOAT NOT NULL, 
    [Stop] FLOAT NOT NULL, 
    [Target] FLOAT NOT NULL, 
    [OpenLevel] FLOAT NOT NULL, 
    [OpenDateTime] DATETIME NOT NULL, 
    [OpenSize] FLOAT NOT NULL, 
    [CloseLevel] FLOAT NULL, 
    [CloseDateTime] DATETIME NULL, 
    [CloseSize] FLOAT NULL, 
    [MaxAdverseExcursion] FLOAT NULL, 
    [MaxFavourableExcursion] FLOAT NULL, 
    CONSTRAINT [FK_Trades_Market] FOREIGN KEY ([MarketId]) REFERENCES [Market]([Id]), 
    CONSTRAINT [FK_Trades_Strategy] FOREIGN KEY ([StrategyId]) REFERENCES [Strategy]([Id])
    )
