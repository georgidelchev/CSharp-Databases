CREATE TABLE Clients (
    ClientId INT NOT NULL IDENTITY(1, 1),
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Phone CHAR(12) NOT NULL,

    CONSTRAINT PK_Clients PRIMARY KEY (ClientId)
)

CREATE TABLE Mechanics (
    MechanicId INT NOT NULL IDENTITY(1, 1),
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Address VARCHAR(255) NOT NULL,

    CONSTRAINT PK_Mechanics PRIMARY KEY (MechanicId)
)

CREATE TABLE Models (
    ModelId INT NOT NULL IDENTITY(1, 1),
    Name VARCHAR(50) NOT NULL,

    CONSTRAINT PK_Models PRIMARY KEY (ModelId),
    CONSTRAINT UQ_Model_Name UNIQUE (Name)
)

CREATE TABLE Vendors (
    VendorId INT NOT NULL IDENTITY(1, 1),
    Name VARCHAR(50) NOT NULL,

    CONSTRAINT PK_Vendors PRIMARY KEY (VendorId),
    CONSTRAINT UQ_Vendor_Name UNIQUE (Name)
)

CREATE TABLE Parts (
    PartId INT NOT NULL IDENTITY(1, 1),
    SerialNumber VARCHAR(50) NOT NULL,
    Description VARCHAR(255),
    Price DECIMAL(6, 2) NOT NULL,
    VendorId INT NOT NULL,
    StockQty INT NOT NULL CONSTRAINT DF_Part_StockQty DEFAULT 0

    CONSTRAINT PK_Parts PRIMARY KEY (PartId),
    CONSTRAINT UQ_Part_SerialNumber UNIQUE (SerialNumber),
    CONSTRAINT CH_Part_Price CHECK (Price > 0),
    CONSTRAINT FK_Parts_Vendors FOREIGN KEY (VendorId) REFERENCES Vendors(VendorId),
    CONSTRAINT CH_Part_StockQty CHECK (StockQty >= 0)
)

CREATE TABLE Jobs (
    JobId INT NOT NULL IDENTITY(1, 1),
    ModelId INT NOT NULL,
    Status VARCHAR(11) NOT NULL CONSTRAINT DF_Job_Status DEFAULT 'Pending',
    ClientId INT NOT NULL,
    MechanicId INT,
    IssueDate DATETIME NOT NULL,
    FinishDate DATETIME

    CONSTRAINT PK_Jobs PRIMARY KEY (JobId),
    CONSTRAINT FK_Jobs_Models FOREIGN KEY (ModelId) REFERENCES Models(ModelId),
    CONSTRAINT CH_Job_Status CHECK (Status IN ('Pending', 'In Progress', 'Finished')),
    CONSTRAINT FK_Jobs_Clients FOREIGN KEY (ClientId) REFERENCES Clients(ClientId),
    CONSTRAINT FK_Jobs_Mechanics FOREIGN KEY (MechanicId) REFERENCES Mechanics(MechanicId)
)

CREATE TABLE Orders (
    OrderId INT NOT NULL IDENTITY(1, 1),
    JobId INT NOT NULL,
    IssueDate DATETIME,
    Delivered BIT NOT NULL CONSTRAINT DF_Order_Delivered DEFAULT 0

    CONSTRAINT PK_Orders PRIMARY KEY (OrderId),
    CONSTRAINT FK_Orders_Jobs FOREIGN KEY (JobId) REFERENCES Jobs(JobId)
)

CREATE TABLE PartsNeeded (
    JobId INT NOT NULL,
    PartId INT NOT NULL,
    Quantity INT NOT NULL CONSTRAINT DF_PartNeeded_Quantity DEFAULT 1,

    CONSTRAINT PK_PartsNeeded PRIMARY KEY (JobId, PartId),
    CONSTRAINT FK_PartsNeeded_Jobs FOREIGN KEY (JobId) REFERENCES Jobs(JobId),
    CONSTRAINT FK_PartsNeeded_Parts FOREIGN KEY (PartId) REFERENCES Parts(PartId),
    CONSTRAINT CH_PartNeeded_Quantity CHECK (Quantity > 0)
)

CREATE TABLE OrderParts (
    OrderId INT NOT NULL,
    PartId INT NOT NULL,
    Quantity INT NOT NULL CONSTRAINT DF_OrderPart_Quantity DEFAULT 1,

    CONSTRAINT PK_OrderParts PRIMARY KEY (OrderId, PartId),
    CONSTRAINT FK_OrderParts_Orders FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    CONSTRAINT FK_OrderParts_Parts FOREIGN KEY (PartId) REFERENCES Parts(PartId),
    CONSTRAINT CH_OrderPart_Quantity CHECK (Quantity > 0)
)