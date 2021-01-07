CREATE DATABASE OnlineStore

USE OnlineStore

CREATE TABLE Orders
(
    OrderID    INT IDENTITY PRIMARY KEY,
    CustomerID INT NOT NULL,

    CONSTRAINT FK_Orders_Customers
        FOREIGN KEY (CustomerID)
            REFERENCES Customers (CustomerID)
)

CREATE TABLE Customers
(
    CustomerID INT IDENTITY PRIMARY KEY,
    Name       VARCHAR(50) NOT NULL,
    Birthday   DATE,
    CityID     INT         NOT NULL,

    CONSTRAINT FK_Customers_Cities
        FOREIGN KEY (CityID)
            REFERENCES Cities (CityID)
)

CREATE TABLE Cities
(
    CityID INT IDENTITY PRIMARY KEY,
    Name   VARCHAR(50) NOT NULL
)

CREATE TABLE OrderItems
(
    OrderID INT IDENTITY PRIMARY KEY,
    ItemID  INT NOT NULL,

    CONSTRAINT FK_OrderItems_Orders
        FOREIGN KEY (OrderID)
            REFERENCES Orders (OrderID),

    CONSTRAINT FK_OrderItems_Items
        FOREIGN KEY (ItemID)
            REFERENCES Items (ItemID)
)

CREATE TABLE Items
(
    ItemID     INT IDENTITY PRIMARY KEY,
    Name       VARCHAR(50) NOT NULL,
    ItemTypeID INT         NOT NULL,

    CONSTRAINT FK_Items_ItemTypes
        FOREIGN KEY (ItemTypeID)
            REFERENCES ItemTypes (ItemTypeID)
)

CREATE TABLE ItemTypes
(
    ItemTypeID INT IDENTITY PRIMARY KEY,
    Name       VARCHAR(50) NOT NULL
)

