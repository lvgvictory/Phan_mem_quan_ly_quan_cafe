CREATE DATABASE QuanLyQuanCafe123
GO

USE QuanLyQuanCafe123
GO

-- Food
-- Table
-- FoodCategory
-- Account
-- Bill
-- BillInfo

CREATE TABLE TableFood
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa đặt tên',
	statuss NVARCHAR(100) NOT NULL DEFAULT N'Trống'	-- Trống || Có người
)
GO

CREATE TABLE Account
(
	UserName NVARCHAR(100) PRIMARY KEY,	
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Hello',
	PassWordd NVARCHAR(1000) NOT NULL DEFAULT 0,
	Typee INT NOT NULL  DEFAULT 0 -- 1: admin && 0: staff
)
GO

CREATE TABLE FoodCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO

CREATE TABLE Food
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
)
GO

CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	idTable INT NOT NULL,
	statuss INT NOT NULL DEFAULT 0 -- 1: đã thanh toán && 0: chưa thanh toán
	
	FOREIGN KEY (idTable) REFERENCES dbo.TableFood(id)
)
GO

CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	countt INT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
	FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
)
GO

INSERT INTO dbo.Account
        ( UserName ,
          DisplayName ,
          PassWordd ,
          Typee
        )
VALUES  ( N'admin' , -- UserName - nvarchar(100)
          N'Adminitrator' , -- DisplayName - nvarchar(100)
          N'1' , -- PassWord - nvarchar(1000)
          1  -- Type - int
        )
INSERT INTO dbo.Account
        ( UserName ,
          DisplayName ,
          PassWordd ,
          Typee
        )
VALUES  ( N'staff' , -- UserName - nvarchar(100)
          N'staff' , -- DisplayName - nvarchar(100)
          N'1' , -- PassWord - nvarchar(1000)
          0  -- Type - int
        )
GO

CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS 
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END
GO

EXEC dbo.USP_GetAccountByUserName @userName = N'admin' -- nvarchar(100)
GO

-- Tạo Provoder cho Login
CREATE PROC USP_Login
@userName NVARCHAR(100), @passWord NVARCHAR(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWordd = @passWord
END
GO

-- thêm dữ liệu vào bảng TableFood
DECLARE @i INT = 0

WHILE @i <= 10
BEGIN
	INSERT dbo.TableFood ( name, statuss)VALUES  ( N'Bàn ' + CAST(@i AS nvarchar(100)), N'Trống')
	SET @i = @i + 1
END
GO

SELECT * FROM dbo.TableFood
GO

-- Tạo Provoder để select dữ liệu bảng TableFood
CREATE PROC USP_GetTableList
AS SELECT * FROM TableFood
GO

EXEC USP_GetTableList

-- INSERT data of FoodCategory
INSERT INTO dbo.FoodCategory
        ( name )
VALUES  ( N'Hải Sản'  -- name - varchar(100)
          )
INSERT INTO dbo.FoodCategory
        ( name )
VALUES  ( N'Nông Sản'  -- name - varchar(100)
          )
INSERT INTO dbo.FoodCategory
        ( name )
VALUES  ( N'lâm Sản'  -- name - varchar(100)
          )
INSERT INTO dbo.FoodCategory
        ( name )
VALUES  ( N'Đồ Uống'  -- name - varchar(100)
          )

GO

SELECT * FROM dbo.FoodCategory
GO

-- Thêm món ăn
INSERT INTO dbo.Food
        ( name, idCategory, price )
VALUES  (
          N'Mực một nắng nướng sa tế', -- name - nvarchar(100)
          1, -- idCategory - int
          120000  -- price - float
          )

INSERT INTO dbo.Food
        (name, idCategory, price )
VALUES  ( 
          N'Ngao hấp xả', -- name - nvarchar(100)
          1, -- idCategory - int
          50000  -- price - float
          )

INSERT INTO dbo.Food
        ( name, idCategory, price )
VALUES  ( 
          N'Lẩu Hải Sản', -- name - nvarchar(100)
          1, -- idCategory - int
          345000  -- price - float
          )

INSERT INTO dbo.Food
        (name, idCategory, price )
VALUES  (
          N'Dú dê nướng sữa', -- name - nvarchar(100)
          2, -- idCategory - int
          75000  -- price - float
          )

INSERT INTO dbo.Food
        (name, idCategory, price )
VALUES  (
          N'Chân gà rang muối', -- name - nvarchar(100)
          2, -- idCategory - int
          90000  -- price - float
          )

INSERT INTO dbo.Food
        (name, idCategory, price )
VALUES  (
          N'Heo rừng nướng', -- name - nvarchar(100)
          3, -- idCategory - int
          120000  -- price - float
          )

INSERT INTO dbo.Food
        (name, idCategory, price )
VALUES  (
          N'7Up', -- name - nvarchar(100)
          4, -- idCategory - int
          15000  -- price - float
          )

INSERT INTO dbo.Food
        (name, idCategory, price )
VALUES  (
          N'Cafe', -- name - nvarchar(100)
          2, -- idCategory - int
          20000  -- price - float
          )

INSERT INTO dbo.Food
        (name, idCategory, price )
VALUES  (
          N'Sinh tố bơ', -- name - nvarchar(100)
          2, -- idCategory - int
          35000  -- price - float
          )
GO

-- INSERT data of Bill
INSERT INTO dbo.Bill
        (
          DateCheckIn ,
          DateCheckOut ,
          idTable ,
          statuss
        )
VALUES  (
          GETDATE() , -- DateCheckIn - date
          NULL , -- DateCheckOut - date
          1 , -- idTable - int
          0  -- statuss - int
        )

INSERT INTO dbo.Bill
        (
          DateCheckIn ,
          DateCheckOut ,
          idTable ,
          statuss
        )
VALUES  (
          GETDATE() , -- DateCheckIn - date
          NULL , -- DateCheckOut - date
          2 , -- idTable - int
          0  -- statuss - int
        )

INSERT INTO dbo.Bill
        (
          DateCheckIn ,
          DateCheckOut ,
          idTable ,
          statuss
        )
VALUES  (
          GETDATE() , -- DateCheckIn - date
          GETDATE() , -- DateCheckOut - date
          2 , -- idTable - int
          1  -- statuss - int
        )
GO

-- INSERT data of BillInfo

INSERT INTO dbo.BillInfo
        (idBill, idFood, countt )
VALUES  (
          1, -- idBill - int
          1, -- idFood - int
          2  -- countt - int
          )

INSERT INTO dbo.BillInfo
        (idBill, idFood, countt )
VALUES  (
          1, -- idBill - int
          3, -- idFood - int
          4  -- countt - int
          )

		  INSERT INTO dbo.BillInfo
        (idBill, idFood, countt )
VALUES  (
          1, -- idBill - int
          4, -- idFood - int
          1  -- countt - int
          )

INSERT INTO dbo.BillInfo
        (idBill, idFood, countt )
VALUES  (
          2, -- idBill - int
          1, -- idFood - int
          2  -- countt - int
          )

INSERT INTO dbo.BillInfo
        (idBill, idFood, countt )
VALUES  (
          2, -- idBill - int
          4, -- idFood - int
          2  -- countt - int
          )
INSERT INTO dbo.BillInfo
        (idBill, idFood, countt )
VALUES  (
          3, -- idBill - int
          3, -- idFood - int
          4  -- countt - int
          )
GO

SELECT * FROM dbo.Bill WHERE idTable = 1 AND statuss = 0
GO
SELECT * FROM dbo.BillInfo WHERE idBill = 3
GO
SELECT * FROM dbo.TableFood
GO

SELECT f.name, bi.countt, f.price, f.price*bi.countt FROM dbo.BillInfo AS bi, dbo.Bill AS b, dbo.Food AS f
WHERE bi.idBill = b.id AND b.statuss = 0 AND bi.idFood = f.id AND b.idTable = 2
GO

SELECT * FROM dbo.Food WHERE idCategory = 2
GO

-- Tạo PROC cho BillDAO
CREATE PROC USP_InsertBill
@idTable INT
AS
BEGIN
	INSERT dbo.Bill
	        ( DateCheckIn ,
	          DateCheckOut ,
	          idTable ,
	          statuss ,
			  discount
	        )
	VALUES  ( GETDATE() , -- DateCheckIn - date
	          NULL , -- DateCheckOut - date
	          @idTable , -- idTable - int
	          0 , -- statuss - int
			  0
	        )
END
GO

-- Tao PROC cho InsertBillInfo
CREATE PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @countt INT
AS
BEGIN

	INSERT dbo.BillInfo
	        ( idBill, idFood, countt )
	VALUES  ( @idBill, -- idBill - int
	          @idFood, -- idFood - int
	          @countt  -- countt - int
	          )

END
GO

SELECT MAX(id) FROM dbo.Bill
GO

-- 
CREATE PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @countt INT
AS
BEGIN

	DECLARE @isExitsBillInfo INT
	DECLARE @foodCount INT = 1

	SELECT @isExitsBillInfo = id, @foodCount = countt FROM dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
	IF(@isExitsBillInfo > 0)
		BEGIN
			DECLARE @newCount INT = @foodCount + @countt
			IF(@newCount > 0)
				UPDATE dbo.BillInfo SET countt = @foodCount + @countt WHERE idFood = @idFood
			ELSE
				DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
		END
	ELSE
		BEGIN
			INSERT dbo.BillInfo
					( idBill, idFood, countt )
			VALUES  ( @idBill, -- idBill - int
					  @idFood, -- idFood - int
					  @countt  -- countt - int
					  )
		END

END
GO

UPDATE dbo.Bill SET statuss = 1 WHERE id = 1
GO

SELECT * FROM dbo.Bill
SELECT * FROM dbo.BillInfo WHERE idBill = 5
GO

DELETE dbo.BillInfo
DELETE dbo.Bill
GO

-- Tạo Trigger cho BillInfo thêm món

ALTER TRIGGER UTG_UpdateBillInfo
ON dbo.BillInfo FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idBill INT

	SELECT @idBill = idBill FROM Inserted

	DECLARE @idTable INT

	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill AND statuss = 0

	DECLARE @count INT
	SELECT @count = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idBill

	IF(@count > 0)
		UPDATE dbo.TableFood SET statuss = N'Có người' WHERE id = @idTable
	ELSE
		UPDATE dbo.TableFood SET statuss = N'Trống' WHERE id = @idTable
END
GO

--CREATE TRIGGER UTG_UpdateTable
--ON dbo.TableFood FOR UPDATE
--AS
--BEGIN

--	DECLARE @idTable INT
--	DECLARE @statuss NVARCHAR(100)

--	SELECT @idTable = id, @statuss = Inserted.statuss FROM Inserted

--	DECLARE @idBill INT
--	SELECT @idBill = id FROM dbo.Bill WHERE idTable = @idTable AND statuss = 0

--	DECLARE @countBillInfo INT
--	SELECT @countBillInfo = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idBill

--	IF(@countBillInfo > 0 AND @statuss <> N'Có người')
--		UPDATE dbo.TableFood SET statuss = N'Có người' WHERE id = @idTable
--	ELSE IF (@countBillInfo <= 0 AND @statuss <> N'Trống')
--		UPDATE dbo.TableFood SET statuss = N'Trống' WHERE id = @idTable

--END
--GO


-- Tạo Trigger update cho bảng Bill thêm món
-- CREATE TRIGGER UTG_UpdateBill
ALTER TRIGGER UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = id FROM Inserted

	DECLARE @idTable INT
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill

	DECLARE @countt INT = 0
	SELECT @countt = COUNT(*) FROM dbo.Bill WHERE idTable = @idTable AND statuss = 0

	IF(@countt = 0)
		UPDATE dbo.TableFood SET statuss = N'Trống' WHERE id = @idTable
END
GO

ALTER TABLE dbo.Bill
ADD discount INT
GO

-- Đặt giá trị mặc định cho biến discount
UPDATE dbo.Bill SET discount = 0
GO

-- Chuyển bàn sửa dụng stored - procedure
ALTER PROC USP_SwitchTable
@idTable1 INT, @idTable2 INT
AS
BEGIN
	DECLARE @idFirstBill INT
	DECLARE @idSeconrdBill INT

	DECLARE @isFirstTableNotEmty INT = 1
	DECLARE @isSeconrdTableNotEmty INT = 1

	SELECT @idSeconrdBill = id FROM dbo.Bill WHERE idTable = @idTable2 AND statuss = 0
	SELECT @idFirstBill = id FROM dbo.Bill WHERE idTable = @idTable1 AND statuss = 0

	IF(@idFirstBill IS NULL) -- nếu @idFirstBill Null thi tạo mới Bill
	BEGIN
		INSERT dbo.Bill
		        ( DateCheckIn ,
		          DateCheckOut ,
		          idTable ,
		          statuss ,
		          discount
		        )
		VALUES  ( GETDATE() , -- DateCheckIn - date
		          NULL , -- DateCheckOut - date
		          @idTable1 , -- idTable - int
		          0 , -- statuss - int
		          0  -- discount - int
		        )

		SELECT @idFirstBill = MAX(id) FROM dbo.Bill WHERE idTable = @idTable1 AND statuss = 0
	END

	SELECT @isFirstTableNotEmty = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idFirstBill

	IF(@idSeconrdBill IS NULL) -- nếu @idFirstBill Null thi tạo mới Bill
	BEGIN
		INSERT dbo.Bill
		        ( DateCheckIn ,
		          DateCheckOut ,
		          idTable ,
		          statuss ,
		          discount
		        )
		VALUES  ( GETDATE() , -- DateCheckIn - date
		          NULL , -- DateCheckOut - date
		          @idTable2 , -- idTable - int
		          0 , -- statuss - int
		          0  -- discount - int
		        )

		SELECT @idSeconrdBill = MAX(id) FROM dbo.Bill WHERE idTable = @idTable2 AND statuss = 0

	END

	SELECT @isSeconrdTableNotEmty = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idSeconrdBill

	SELECT id INTO IDBillInfoTable FROM dbo.BillInfo WHERE idBill = @idSeconrdBill -- Tạo bảng IDBillInfoTable để lưu trữ thông tin id
	UPDATE dbo.BillInfo SET idBill = @idSeconrdBill WHERE idBill = @idFirstBill
	UPDATE dbo.BillInfo SET idBill = @idFirstBill WHERE id IN (SELECT * FROM IDBillInfoTable)

	DROP TABLE IDBillInfoTable

	IF(@isFirstTableNotEmty = 0)
		UPDATE dbo.TableFood SET statuss = N'Trống' WHERE id = @idTable2
	IF(@isSeconrdTableNotEmty = 0)
		UPDATE dbo.TableFood SET statuss = N'Trống' WHERE id = @idTable1

END
GO

--DECLARE @idBillNew INT = 19
--SELECT id INTO IDBillInfoTable FROM dbo.BillInfo WHERE idBill = @idBillNew -- Tạo bảng IDBillInfoTable để lưu trữ thông tin id

--DECLARE @idBillOld INT = 10
--UPDATE dbo.BillInfo SET idBill = @idBillOld WHERE id IN (SELECT * FROM IDBillInfoTable)
--GO

ALTER TABLE dbo.Bill ADD totalPrice FLOAT
DELETE dbo.Bill

SELECT t.name,b.totalPrice, DateCheckIn, DateCheckOut, discount 
FROM dbo.Bill AS b, dbo.TableFood AS t
WHERE 
	DateCheckIn >= '20170901'
	AND DateCheckOut <= '20170930'
	AND b.statuss = 1
	AND b.idTable = t.id
GO

-- Tạo PROC cho Doanh Thu theo ngày
CREATE PROC USP_GetListBillByDate
@checkIn DATE, @checkOut DATE
AS
BEGIN
	SELECT t.name AS [Tên bàn],b.totalPrice AS [Tổng tiền], DateCheckIn AS [Ngày vào], DateCheckOut AS [Ngày ra], discount AS [Giảm giá] 
	FROM dbo.Bill AS b, dbo.TableFood AS t
	WHERE 
		DateCheckIn >= @checkIn
		AND DateCheckOut <= @checkOut
		AND b.statuss = 1
		AND b.idTable = t.id
END
GO

--Tao PROC cho from UpdateAccount
CREATE PROC USP_UpdateAccount
@userName NVARCHAR(100), @displayName NVARCHAR(100), @passwordd NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN

	DECLARE @isRightPass INT = 0
	SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE UserName = @userName AND PassWordd = @passwordd

	IF(@isRightPass = 1)
	BEGIN
		IF(@newPassword = NULL OR @newPassword = '')
			UPDATE dbo.Account SET DisplayName = @displayName WHERE UserName = @userName
		ELSE
			UPDATE dbo.Account SET DisplayName = @displayName, PassWordd = @newPassword WHERE UserName = @userName
	END

END
GO

-- Tạo Trigger delete billInfo
CREATE TRIGGER UTG_BillInfo
ON  dbo.BillInfo FOR DELETE
AS
BEGIN

	DECLARE @idBillInfo INT
	DECLARE @idBill INT
	SELECT @idBillInfo = id, @idBill = Deleted.idBill FROM Deleted

	DECLARE @idTable INT
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill

	DECLARE @count INT = 0
	SELECT @count = COUNT(*) FROM dbo.BillInfo AS bi, dbo.Bill AS b WHERE b.id = bi.idBill AND b.id = @idBill AND b.statuss = 0

	IF(@count = 0)
		UPDATE dbo.TableFood SET statuss = N'Trống' WHERE id = @idTable
END
GO

-- Hàm chuyển đổi từ có dấu sang không dấu
CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
GO
-- Câu lệnh tìm kiếm thức ăn
SELECT * FROM dbo.Food WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1(N'muc') + '%'
GO

SELECT UserName, DisplayName, Typee FROM dbo.Account