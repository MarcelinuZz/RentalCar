CREATE DATABASE rentcar;

USE rentcar;

SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE';

CREATE TABLE MsCar (
	Car_id nvarchar(36) NOT NULL,
	name nvarchar(200)  NULL,
	model nvarchar(100)  NULL,
	[year] int NULL,
	license_plate nvarchar(50)  NULL,
	number_of_car_seats int NULL,
	transmission nvarchar(100)  NULL,
	price_per_day decimal(18,2) NULL,
	status bit NULL,
	CONSTRAINT PK__MsCar__52395FD1DD1623D1 PRIMARY KEY (Car_id)
);

CREATE TABLE MsCustomer (
	Customer_id nvarchar(36) NOT NULL,
	email nvarchar(100) NOT NULL,
	password nvarchar(100) NOT NULL,
	name nvarchar(200) NOT NULL,
	phone_number nvarchar(50) NULL,
	address nvarchar(500) NULL,
	driver_license_number nvarchar(100) NULL,
	CONSTRAINT PK__MsCustom__8CB382B1DA4CC8D5 PRIMARY KEY (Customer_id)
);

CREATE TABLE MsEmployee (
	Employee_id nvarchar(36) NOT NULL,
	name nvarchar(200) NOT NULL,
	[position] nvarchar(4000) NULL,
	email nvarchar(200) NULL,
	phone_number nvarchar(36) NULL,
	CONSTRAINT PK__MsEmploy__781228D974A19982 PRIMARY KEY (Employee_id)
);

CREATE TABLE MsCarImages (
	Image_car_id nvarchar(36) NOT NULL,
	Car_id nvarchar(36) NOT NULL,
	image_link nvarchar(2000) NULL,
	CONSTRAINT PK__MsCarIma__ADBC96273B1F0878 PRIMARY KEY (Image_car_id),
	CONSTRAINT FK__MsCarImag__Car_i__46E78A0C FOREIGN KEY (Car_id) REFERENCES MsCar(Car_id)
);

CREATE TABLE TrMaintenance (
	Maintenance_id nvarchar(36) NOT NULL,
	maintenance_date datetime2 NULL,
	description nvarchar(4000) NULL,
	cost decimal(18,2) NULL,
	car_id nvarchar(36)NOT NULL,
	employee_id nvarchar(36)NOT NULL,
	CONSTRAINT PK__TrMainte__CCA51FA78B21E7C8 PRIMARY KEY (Maintenance_id),
	CONSTRAINT FK__TrMainten__car_i__4316F928 FOREIGN KEY (car_id) REFERENCES MsCar(Car_id),
	CONSTRAINT FK__TrMainten__emplo__440B1D61 FOREIGN KEY (employee_id) REFERENCES MsEmployee(Employee_id)
);

CREATE TABLE TrRental (
	Rental_id nvarchar(36) NOT NULL,
	rental_date datetime2 NOT NULL,
	return_date datetime2 NULL,
	total_price decimal(18,2) NULL,
	payment_status bit NULL,
	customer_id nvarchar(36) NOT NULL,
	car_id nvarchar(36) NOT NULL,
	CONSTRAINT PK__TrRental__9D20A03EBF374A49 PRIMARY KEY (Rental_id),
	CONSTRAINT FK__TrRental__car_id__3D5E1FD2 FOREIGN KEY (car_id) REFERENCES MsCar(Car_id),
	CONSTRAINT FK__TrRental__custom__3C69FB99 FOREIGN KEY (customer_id) REFERENCES MsCustomer(Customer_id)
);

CREATE TABLE TrPayment (
	Payment_id nvarchar(36) NOT NULL,
	payment_date datetime2 NULL,
	amount decimal(18,2) NULL,
	payment_method nvarchar(100) NULL,
	rental_id nvarchar(36) NOT NULL,
	CONSTRAINT PK__TrPaymen__DA638B1991CB7BAE PRIMARY KEY (Payment_id),
	CONSTRAINT FK__TrPayment__renta__403A8C7D FOREIGN KEY (rental_id) REFERENCES TrRental(Rental_id)
);

-- menambahkan index agar query lebih cepat, mohon jalankan dengan blok satu per satu
CREATE INDEX IDX_TrMaintenance_CarId ON TrMaintenance (car_id);
CREATE INDEX IDX_MsCar_CarId ON MsCar (Car_id);
CREATE INDEX IDX_TrMaintenance_MaintenanceDate ON TrMaintenance (maintenance_date);
CREATE INDEX IDX_TrMaintenance_CarId_MaintenanceDate ON TrMaintenance (car_id, maintenance_date);

INSERT INTO MsCar (Car_id, name, model, [year], license_plate, number_of_car_seats, transmission, price_per_day, status)
VALUES
('CAR001', 'Ferrari LaFerrari', 'Model 1', 2016, 'B0001 XYZ', 5, 'Automatic', 51500.00, 1),
('CAR002', 'Lamborghini Aventador', 'Model 2', 2017, 'B0002 XYZ', 6, 'Automatic', 53000.00, 1),
('CAR003', 'Porsche 911 Turbo', 'Model 3', 2018, 'B0003 XYZ', 4, 'Automatic', 54500.00, 1),
('CAR004', 'McLaren P1', 'Model 4', 2019, 'B0004 XYZ', 5, 'Automatic', 56000.00, 1),
('CAR005', 'Bugatti Chiron', 'Model 5', 2015, 'B0005 XYZ', 6, 'Automatic', 57500.00, 1),
('CAR006', 'Aston Martin DB11', 'Model 6', 2016, 'B0006 XYZ', 4, 'Automatic', 59000.00, 1),
('CAR007', 'Tesla Model S Plaid', 'Model 7', 2017, 'B0007 XYZ', 5, 'Automatic', 60500.00, 1),
('CAR008', 'Ford Mustang Shelby GT500', 'Model 8', 2018, 'B0008 XYZ', 6, 'Manual', 62000.00, 1),
('CAR009', 'Chevrolet Corvette C8', 'Model 9', 2019, 'B0009 XYZ', 4, 'Automatic', 63500.00, 1),
('CAR010', 'BMW M4 GTS', 'Model 10', 2015, 'B0010 XYZ', 5, 'Manual', 65000.00, 1),
('CAR011', 'Audi R8 V10 Plus', 'Model 11', 2016, 'B0011 XYZ', 6, 'Automatic', 66500.00, 1),
('CAR012', 'Mercedes-Benz AMG GT R', 'Model 12', 2017, 'B0012 XYZ', 4, 'Automatic', 68000.00, 1),
('CAR013', 'Dodge Challenger SRT Hellcat', 'Model 13', 2018, 'B0013 XYZ', 5, 'Manual', 69500.00, 1),
('CAR014', 'Nissan GT-R Nismo', 'Model 14', 2019, 'B0014 XYZ', 6, 'Automatic', 71000.00, 1),
('CAR015', 'Lamborghini Huracán EVO', 'Model 15', 2015, 'B0015 XYZ', 4, 'Automatic', 72500.00, 1),
('CAR016', 'Pagani Huayra', 'Model 16', 2016, 'B0016 XYZ', 5, 'Automatic', 74000.00, 1),
('CAR017', 'Koenigsegg Jesko', 'Model 17', 2017, 'B0017 XYZ', 6, 'Automatic', 75500.00, 1),
('CAR018', 'Ford GT', 'Model 18', 2018, 'B0018 XYZ', 4, 'Automatic', 77000.00, 1),
('CAR019', 'Ferrari LaFerrari', 'Model 1', 2020, 'B0019 XYZ', 5, 'Automatic', 51500.00, 1),
('CAR020', 'Lamborghini Aventador', 'Model 2', 2021, 'B0020 XYZ', 6, 'Automatic', 53000.00, 1),
('CAR021', 'Porsche 911 Turbo', 'Model 3', 2022, 'B0021 XYZ', 4, 'Automatic', 54500.00, 1),
('CAR022', 'McLaren P1', 'Model 4', 2023, 'B0022 XYZ', 5, 'Automatic', 56000.00, 1),
('CAR023', 'Bugatti Chiron', 'Model 5', 2020, 'B0023 XYZ', 6, 'Automatic', 57500.00, 1),
('CAR024', 'Aston Martin DB11', 'Model 6', 2021, 'B0024 XYZ', 4, 'Automatic', 59000.00, 1),
('CAR025', 'Tesla Model S Plaid', 'Model 7', 2022, 'B0025 XYZ', 5, 'Automatic', 60500.00, 1),
('CAR026', 'Ford Mustang Shelby GT500', 'Model 8', 2023, 'B0026 XYZ', 6, 'Manual', 62000.00, 1),
('CAR027', 'Chevrolet Corvette C8', 'Model 9', 2020, 'B0027 XYZ', 4, 'Automatic', 63500.00, 1),
('CAR028', 'Mercedes-Benz AMG GT R', 'Model 12', 2022, 'B0028 XYZ', 4, 'Automatic', 68000.00, 1),
('CAR029', 'Dodge Challenger SRT Hellcat', 'Model 13', 2023, 'B0029 XYZ', 5, 'Manual', 69500.00, 1),
('CAR030', 'Nissan GT-R Nismo', 'Model 14', 2021, 'B0030 XYZ', 6, 'Automatic', 71000.00, 1),
('CAR031', 'Lamborghini Huracán EVO', 'Model 15', 2022, 'B0031 XYZ', 4, 'Automatic', 72500.00, 1),
('CAR032', 'Pagani Huayra', 'Model 16', 2023, 'B0032 XYZ', 5, 'Automatic', 74000.00, 1);

INSERT INTO MsCustomer (Customer_id, email, password, name, phone_number, address, driver_license_number)
VALUES
('CU001', 'john.doe@email.com', 'pass1234', 'John Doe', '08123456789', 'Jl. Sudirman No.1, Jakarta', 'DL123456'),
('CU002', 'jane.smith@email.com', 'securepass', 'Jane Smith', '08234567890', 'Jl. Thamrin No.2, Jakarta', 'DL654321'),
('CU003', 'michael.johnson@email.com', 'mikepass', 'Michael Johnson', '08345678901', 'Jl. Gatot Subroto No.5, Jakarta', 'DL789012'),
('CU004', 'emily.watson@email.com', 'watson123', 'Emily Watson', '08456789012', 'Jl. Kuningan No.3, Jakarta', 'DL345678'),
('CU005', 'david.miller@email.com', 'millerpass', 'David Miller', '08567890123', 'Jl. Senayan No.8, Jakarta', 'DL901234'),
('CU006', 'sarah.conner@email.com', 'terminator', 'Sarah Conner', '08678901234', 'Jl. Menteng No.10, Jakarta', 'DL567890'),
('CU007', 'peter.parker@email.com', 'spideypass', 'Peter Parker', '08789012345', 'Jl. Monas No.2, Jakarta', 'DL432109'),
('CU008', 'clark.kent@email.com', 'superman', 'Clark Kent', '08890123456', 'Jl. Kebayoran No.12, Jakarta', 'DL876543');

INSERT INTO MsEmployee (Employee_id, name, [position], email, phone_number)
VALUES
('EMP01', 'Employee 1', 'Staff', 'employee1@example.com', '123-456-7801'),
('EMP02', 'Employee 2', 'Staff', 'employee2@example.com', '123-456-7802'),
('EMP03', 'Employee 3', 'Staff', 'employee3@example.com', '123-456-7803'),
('EMP04', 'Employee 4', 'Staff', 'employee4@example.com', '123-456-7804'),
('EMP05', 'Employee 5', 'Staff', 'employee5@example.com', '123-456-7805'),
('EMP06', 'Employee 6', 'Staff', 'employee6@example.com', '123-456-7806'),
('EMP07', 'Employee 7', 'Staff', 'employee7@example.com', '123-456-7807'),
('EMP08', 'Employee 8', 'Staff', 'employee8@example.com', '123-456-7808'),
('EMP09', 'Employee 9', 'Staff', 'employee9@example.com', '123-456-7809'),
('EMP10', 'Employee 10', 'Staff', 'employee10@example.com', '123-456-7810');

INSERT INTO MsCarImages (Image_car_id, Car_id, image_link)
VALUES
('IMG001', 'CAR001', 'Car_IMG1.jpg'),
('IMG002', 'CAR002', 'Car_IMG2.jpg'),
('IMG003', 'CAR003', 'Car_IMG3.jpg'),
('IMG004', 'CAR004', 'Car_IMG4.jpg'),
('IMG005', 'CAR005', 'Car_IMG5.jpg'),
('IMG006', 'CAR006', 'Car_IMG6.jpg'),
('IMG007', 'CAR007', 'Car_IMG7.jpg'),
('IMG008', 'CAR008', 'Car_IMG8.jpg'),
('IMG009', 'CAR009', 'Car_IMG9.jpg'),
('IMG010', 'CAR010', 'Car_IMG10.jpg'),
('IMG011', 'CAR011', 'Car_IMG11.jpg'),
('IMG012', 'CAR012', 'Car_IMG12.jpg'),
('IMG013', 'CAR013', 'Car_IMG13.jpg'),
('IMG014', 'CAR014', 'Car_IMG14.jpg'),
('IMG015', 'CAR015', 'Car_IMG15.jpg'),
('IMG016', 'CAR016', 'Car_IMG16.jpg'),
('IMG017', 'CAR017', 'Car_IMG17.jpg'),
('IMG018', 'CAR018', 'Car_IMG18.jpg'),
('IMG019', 'CAR019', 'Car_IMG1.jpg'),
('IMG020', 'CAR020', 'Car_IMG2.jpg'),
('IMG021', 'CAR021', 'Car_IMG3.jpg'),
('IMG022', 'CAR022', 'Car_IMG4.jpg'),
('IMG023', 'CAR023', 'Car_IMG5.jpg'),
('IMG024', 'CAR024', 'Car_IMG6.jpg'),
('IMG025', 'CAR025', 'Car_IMG7.jpg'),
('IMG026', 'CAR026', 'Car_IMG8.jpg'),
('IMG027', 'CAR027', 'Car_IMG9.jpg'),
('IMG028', 'CAR028', 'Car_IMG12.jpg'),
('IMG029', 'CAR029', 'Car_IMG13.jpg'),
('IMG030', 'CAR030', 'Car_IMG14.jpg'),
('IMG031', 'CAR031', 'Car_IMG15.jpg'),
('IMG032', 'CAR032', 'Car_IMG16.jpg');

INSERT INTO TrMaintenance (Maintenance_id, maintenance_date, description, cost, car_id, employee_id)
VALUES
('M001', '2024-01-05', 'Oil change and tire rotation', 250000.00, 'CAR001', 'EMP01'),
('M002', '2024-01-12', 'Brake pad replacement', 500000.00, 'CAR002', 'EMP02'),
('M003', '2024-01-18', 'Engine tuning and diagnostic', 750000.00, 'CAR003', 'EMP03'),
('M004', '2024-01-25', 'Full detailing and wax', 300000.00, 'CAR004', 'EMP04'),
('M005', '2024-02-01', 'Transmission fluid change', 650000.00, 'CAR005', 'EMP05'),
('M006', '2024-02-10', 'Battery replacement', 400000.00, 'CAR006', 'EMP06'),
('M007', '2024-02-15', 'Suspension check and alignment', 550000.00, 'CAR007', 'EMP07'),
('M008', '2024-02-20', 'Radiator service and coolant refill', 600000.00, 'CAR008', 'EMP08'),
('M009', '2024-02-28', 'Fuel injection system cleaning', 700000.00, 'CAR009', 'EMP09'),
('M010', '2024-03-05', 'AC system recharge', 350000.00, 'CAR010', 'EMP10');

INSERT INTO TrRental (Rental_id, rental_date, return_date, total_price, payment_status, customer_id, car_id)
VALUES
('R001', '2024-01-02', '2024-01-05', 154500.00, 1, 'CU001', 'CAR001'),
('R002', '2024-01-10', '2024-01-14', 212000.00, 1, 'CU002', 'CAR002'),
('R003', '2024-01-15', '2024-01-18', 187500.00, 0, 'CU003', 'CAR003'),
('R004', '2024-01-20', '2024-01-24', 256000.00, 1, 'CU004', 'CAR004'),
('R005', '2024-01-25', '2024-01-30', 315000.00, 0, 'CU005', 'CAR005'),
('R006', '2024-02-01', '2024-02-05', 192500.00, 1, 'CU006', 'CAR006'),
('R007', '2024-02-07', '2024-02-10', 163500.00, 1, 'CU007', 'CAR007'),
('R008', '2024-02-12', '2024-02-16', 242000.00, 0, 'CU008', 'CAR008'),
('R009', '2024-02-18', '2024-02-22', 290000.00, 1, 'CU001', 'CAR009'),
('R010', '2024-02-25', '2024-03-01', 370000.00, 0, 'CU002', 'CAR010'),
('R011', '2024-03-02', '2024-03-06', 200000.00, 1, 'CU003', 'CAR011'),
('R012', '2024-03-05', '2024-03-09', 225000.00, 0, 'CU004', 'CAR012'),
('R013', '2024-03-08', '2024-03-12', 260000.00, 1, 'CU005', 'CAR013'),
('R014', '2024-03-10', '2024-03-15', 310000.00, 1, 'CU006', 'CAR014'),
('R015', '2024-03-15', '2024-03-19', 280000.00, 0, 'CU007', 'CAR015'),
('R016', '2024-03-18', '2024-03-22', 330000.00, 1, 'CU008', 'CAR016'),
('R017', '2024-03-21', '2024-03-25', 355000.00, 1, 'CU001', 'CAR017'),
('R018', '2024-03-23', '2024-03-28', 390000.00, 0, 'CU002', 'CAR018'),
('R019', '2024-03-26', '2024-03-30', 405000.00, 1, 'CU003', 'CAR019'),
('R020', '2024-03-28', '2024-04-02', 420000.00, 0, 'CU004', 'CAR020'),
('R021', '2024-04-01', '2024-04-05', 440000.00, 1, 'CU005', 'CAR021'),
('R022', '2024-04-04', '2024-04-08', 460000.00, 1, 'CU006', 'CAR022'),
('R023', '2024-04-07', '2024-04-11', 480000.00, 0, 'CU007', 'CAR023'),
('R024', '2024-04-09', '2024-04-14', 500000.00, 1, 'CU008', 'CAR024'),
('R025', '2024-04-12', '2024-04-16', 520000.00, 1, 'CU001', 'CAR025'),
('R026', '2024-04-14', '2024-04-19', 540000.00, 0, 'CU002', 'CAR026'),
('R027', '2024-04-17', '2024-04-21', 560000.00, 1, 'CU003', 'CAR027'),
('R028', '2024-04-20', '2024-04-24', 580000.00, 0, 'CU004', 'CAR028'),
('R029', '2024-04-22', '2024-04-26', 600000.00, 1, 'CU005', 'CAR029'),
('R030', '2024-04-25', '2024-04-29', 620000.00, 1, 'CU006', 'CAR030');

INSERT INTO TrPayment (Payment_id, payment_date, amount, payment_method, rental_id)
VALUES
('P001', '2024-01-05', 154500.00, 'Credit Card', 'R001'),
('P002', '2024-01-14', 212000.00, 'Bank Transfer', 'R002'),
('P003', '2024-01-24', 256000.00, 'PayPal', 'R004'),
('P004', '2024-02-05', 192500.00, 'Cash', 'R006'),
('P005', '2024-02-10', 163500.00, 'Credit Card', 'R007'),
('P006', '2024-02-22', 290000.00, 'Bank Transfer', 'R009'),
('P007', '2024-03-01', 370000.00, 'PayPal', 'R010'),
('P008', '2024-03-06', 200000.00, 'Credit Card', 'R011'),
('P009', '2024-03-12', 260000.00, 'Bank Transfer', 'R013'),
('P010', '2024-03-15', 310000.00, 'Cash', 'R014'),
('P011', '2024-03-22', 330000.00, 'PayPal', 'R016'),
('P012', '2024-03-25', 355000.00, 'Bank Transfer', 'R017'),
('P013', '2024-03-30', 405000.00, 'Credit Card', 'R019'),
('P014', '2024-04-05', 440000.00, 'Cash', 'R021'),
('P015', '2024-04-08', 460000.00, 'PayPal', 'R022'),
('P016', '2024-04-14', 500000.00, 'Credit Card', 'R024'),
('P017', '2024-04-16', 520000.00, 'Bank Transfer', 'R025'),
('P018', '2024-04-21', 560000.00, 'Cash', 'R027'),
('P019', '2024-04-26', 600000.00, 'PayPal', 'R029'),
('P020', '2024-04-29', 620000.00, 'Bank Transfer', 'R030');

select * from MsCar
select * from MsCustomer
select * from MsCarImages
select * from MsEmployee
select * from TrMaintenance
select * from TrRental
select * from TrPayment