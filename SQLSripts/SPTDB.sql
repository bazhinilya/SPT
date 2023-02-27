CREATE DATABASE SoftTradePlus
GO
USE SoftTradePlus
GO

CREATE TABLE Managers
(
    Id INT IDENTITY(100, 1) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL
)

CREATE TABLE Clients
(
    Id INT IDENTITY(100, 1) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    ClientStatus NVARCHAR(100) NOT NULL,
    ManagerId INT NOT NULL FOREIGN KEY REFERENCES Managers(Id)
)

CREATE TABLE Products
(
    Id INT IDENTITY(100, 1) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    Price DECIMAL(12, 2) NOT NULL,
    [Type] NVARCHAR(100) NOT NULL,
    SubscriptionPeriod INT NULL
)

CREATE TABLE ClientsProducts
(
    Id INT IDENTITY(100, 1) PRIMARY KEY,
    ProductId INT NOT NULL CONSTRAINT [FK_ClientsProducts_Products] FOREIGN KEY REFERENCES Products (Id),
    ClientId INT NOT NULL CONSTRAINT [FK_ClientsProducts_Clients] FOREIGN KEY REFERENCES Clients (Id)
)

INSERT Managers(Name)
VALUES
    ('Иванов Иван Иванович'),
    ('Петров Петр Петрович'),
    ('Сидоров Сидр Сидорович')

INSERT Clients(Name, ClientStatus, ManagerId)
VALUES 
    ('Александров Александр Александрович', 'Ключевой клиент', (SELECT TOP 1 m1.Id FROM Managers m1 ORDER BY NEWID())),
    ('Алексеев Алексей Алексеевич', 'Обычный клиент', (SELECT TOP 1 m1.Id FROM Managers m1 ORDER BY NEWID())),
    ('Андреев Андрей Андреевич', 'Ключевой клиент', (SELECT TOP 1 m1.Id FROM Managers m1 ORDER BY NEWID())),
    ('Владимиров Владимир Владимирович', 'Обычный клиент', (SELECT TOP 1 m1.Id FROM Managers m1 ORDER BY NEWID()))

INSERT Products(Name, [Type], Price, SubscriptionPeriod)
VALUES
    ('Продукт 1', 'Подписка', 1000, 30),
    ('Продукт 2', 'Постоянная лицензия', 10000, ''),
    ('Продукт 3', 'Подписка', 2000, 60),
    ('Продукт 4', 'Подписка', 3000, 90)

INSERT ClientsProducts(ClientId, ProductId)
VALUES
    ((SELECT TOP 1 c1.Id FROM Clients c1 ORDER BY NEWID()), (SELECT TOP 1 p1.Id FROM Products p1 ORDER BY NEWID())),
    ((SELECT TOP 1 c1.Id FROM Clients c1 ORDER BY NEWID()), (SELECT TOP 1 p1.Id FROM Products p1 ORDER BY NEWID())),
    ((SELECT TOP 1 c1.Id FROM Clients c1 ORDER BY NEWID()), (SELECT TOP 1 p1.Id FROM Products p1 ORDER BY NEWID())),
    ((SELECT TOP 1 c1.Id FROM Clients c1 ORDER BY NEWID()), (SELECT TOP 1 p1.Id FROM Products p1 ORDER BY NEWID())),
    ((SELECT TOP 1 c1.Id FROM Clients c1 ORDER BY NEWID()), (SELECT TOP 1 p1.Id FROM Products p1 ORDER BY NEWID())),
    ((SELECT TOP 1 c1.Id FROM Clients c1 ORDER BY NEWID()), (SELECT TOP 1 p1.Id FROM Products p1 ORDER BY NEWID())),
    ((SELECT TOP 1 c1.Id FROM Clients c1 ORDER BY NEWID()), (SELECT TOP 1 p1.Id FROM Products p1 ORDER BY NEWID()))