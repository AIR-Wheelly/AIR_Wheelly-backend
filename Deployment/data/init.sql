CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Locations" (
    "LocationId" uuid NOT NULL,
    "Latitude" double precision NOT NULL,
    "Longitude" double precision NOT NULL,
    "Adress" text NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    "UpdatedAt" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Locations" PRIMARY KEY ("LocationId")
);

CREATE TABLE "Manafacturers" (
    "Id" uuid NOT NULL,
    "Name" character varying(50) NOT NULL,
    CONSTRAINT "PK_Manafacturers" PRIMARY KEY ("Id")
);

CREATE TABLE "Users" (
    "Id" uuid NOT NULL,
    "FirstName" character varying(50) NOT NULL,
    "LastName" character varying(50) NOT NULL,
    "Email" character varying(100) NOT NULL,
    "Password" text NOT NULL,
    "CreatedAt" timestamp NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

CREATE TABLE "Models" (
    "Id" uuid NOT NULL,
    "ManafacturerId" uuid NOT NULL,
    "Name" character varying(50) NOT NULL,
    CONSTRAINT "PK_Models" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Models_Manafacturers_ManafacturerId" FOREIGN KEY ("ManafacturerId") REFERENCES "Manafacturers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CarListings" (
    "Id" uuid NOT NULL,
    "ModelId" uuid NOT NULL,
    "YearOfProduction" integer NOT NULL,
    "NumberOfSeats" integer NOT NULL,
    "FuelType" text NOT NULL,
    "RentalPriceType" double precision NOT NULL,
    "LocationId" uuid NOT NULL,
    "NumberOfKilometers" double precision NOT NULL,
    "RegistrationNumber" text NOT NULL,
    "Description" character varying(10000) NOT NULL,
    "IsActive" boolean NOT NULL,
    "UserId" uuid NOT NULL,
    CONSTRAINT "PK_CarListings" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CarListings_Locations_LocationId" FOREIGN KEY ("LocationId") REFERENCES "Locations" ("LocationId") ON DELETE CASCADE,
    CONSTRAINT "FK_CarListings_Models_ModelId" FOREIGN KEY ("ModelId") REFERENCES "Models" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CarListingPictures" (
    "Id" uuid NOT NULL,
    "CarListingId" uuid NOT NULL,
    "Image" text NOT NULL,
    CONSTRAINT "PK_CarListingPictures" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CarListingPictures_CarListings_CarListingId" FOREIGN KEY ("CarListingId") REFERENCES "CarListings" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_CarListingPictures_CarListingId" ON "CarListingPictures" ("CarListingId");

CREATE INDEX "IX_CarListings_LocationId" ON "CarListings" ("LocationId");

CREATE INDEX "IX_CarListings_ModelId" ON "CarListings" ("ModelId");

CREATE INDEX "IX_Models_ManafacturerId" ON "Models" ("ManafacturerId");

CREATE UNIQUE INDEX "IX_Users_Email" ON "Users" ("Email");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241212223912_Create', '9.0.0-rc.2.24474.1');

CREATE INDEX "IX_CarListings_UserId" ON "CarListings" ("UserId");

ALTER TABLE "CarListings" ADD CONSTRAINT "FK_CarListings_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241216200001_connect-car-listing-and-user-table', '9.0.0-rc.2.24474.1');

CREATE TABLE "CarReservations" (
    "Id" uuid NOT NULL,
    "CarListingId" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "StartDate" timestamp without time zone NOT NULL,
    "EndDate" timestamp without time zone NOT NULL,
    "TotalPrice" double precision NOT NULL,
    "Status" integer NOT NULL,
    "IsPaid" boolean NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_CarReservations" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CarReservations_CarListings_CarListingId" FOREIGN KEY ("CarListingId") REFERENCES "CarListings" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_CarReservations_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_CarReservations_CarListingId" ON "CarReservations" ("CarListingId");

CREATE INDEX "IX_CarReservations_UserId" ON "CarReservations" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250109222907_create-table-reservation', '9.0.0-rc.2.24474.1');

CREATE TABLE "Review" (
    "UserId" uuid NOT NULL,
    "ListingId" uuid NOT NULL,
    "Grade" integer NOT NULL,
    "CarListingId" uuid NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Review" PRIMARY KEY ("UserId", "ListingId"),
    CONSTRAINT "FK_Review_CarListings_CarListingId" FOREIGN KEY ("CarListingId") REFERENCES "CarListings" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Review_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Review_CarListingId" ON "Review" ("CarListingId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250127190244_AddReviewsTable', '9.0.0-rc.2.24474.1');

ALTER TABLE "Review" DROP CONSTRAINT "FK_Review_Users_UserId";

ALTER TABLE "Review" DROP CONSTRAINT "PK_Review";

ALTER TABLE "Review" DROP COLUMN "ListingId";

ALTER TABLE "Review" ADD CONSTRAINT "PK_Review" PRIMARY KEY ("UserId", "CarListingId");

ALTER TABLE "Review" ADD CONSTRAINT "FK_Review_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE SET NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250127195507_FixReviewRelationships', '9.0.0-rc.2.24474.1');

COMMIT;


INSERT INTO "Manafacturers" ("Id", "Name")
VALUES
    (gen_random_uuid(), 'Toyota'),
    (gen_random_uuid(), 'Volkswagen'),
    (gen_random_uuid(), 'Ford'),
    (gen_random_uuid(), 'Honda'),
    (gen_random_uuid(), 'BMW'),
    (gen_random_uuid(), 'Mercedes-Benz'),
    (gen_random_uuid(), 'Chevrolet'),
    (gen_random_uuid(), 'Nissan'),
    (gen_random_uuid(), 'Hyundai'),
    (gen_random_uuid(), 'Kia'),
    (gen_random_uuid(), 'Peugeot'),
    (gen_random_uuid(), 'Fiat'),
    (gen_random_uuid(), 'Renault'),
    (gen_random_uuid(), 'Audi'),
    (gen_random_uuid(), 'Mazda'),
    (gen_random_uuid(), 'Subaru'),
    (gen_random_uuid(), 'Volvo'),
    (gen_random_uuid(), 'Jeep'),
    (gen_random_uuid(), 'Jaguar'),
    (gen_random_uuid(), 'Land Rover'),
    (gen_random_uuid(), 'Skoda'),
    (gen_random_uuid(), 'Seat'),
    (gen_random_uuid(), 'Tesla'),
    (gen_random_uuid(), 'Suzuki'),
    (gen_random_uuid(), 'Dodge'),
    (gen_random_uuid(), 'Chrysler'),
    (gen_random_uuid(), 'Mitsubishi'),
    (gen_random_uuid(), 'Mini'),
    (gen_random_uuid(), 'Lexus'),
    (gen_random_uuid(), 'Alfa Romeo'),
    (gen_random_uuid(), 'Ferrari'),
    (gen_random_uuid(), 'Lamborghini'),
    (gen_random_uuid(), 'Porsche'),
    (gen_random_uuid(), 'Aston Martin'),
    (gen_random_uuid(), 'Bentley'),
    (gen_random_uuid(), 'Bugatti'),
    (gen_random_uuid(), 'Rolls Royce'),
    (gen_random_uuid(), 'McLaren'),
    (gen_random_uuid(), 'Pagani'),
    (gen_random_uuid(), 'Citroën'),
    (gen_random_uuid(), 'Saab'),
    (gen_random_uuid(), 'Isuzu'),
    (gen_random_uuid(), 'Hummer'),
    (gen_random_uuid(), 'Opel'),
    (gen_random_uuid(), 'Buick'),
    (gen_random_uuid(), 'GMC'),
    (gen_random_uuid(), 'Cadillac'),
    (gen_random_uuid(), 'Infinity'),
    (gen_random_uuid(), 'Acura');
  INSERT INTO "Models" ("Id", "ManafacturerId", "Name")
VALUES
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Toyota'), 'Corolla'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Toyota'), 'Camry'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Toyota'), 'RAV4'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Volkswagen'), 'Golf'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Volkswagen'), 'Passat'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Volkswagen'), 'Tiguan'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Ford'), 'Fiesta'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Ford'), 'Focus'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Ford'), 'Explorer'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Honda'), 'Civic'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Honda'), 'Accord'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Honda'), 'CR-V'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'BMW'), '3 Series'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'BMW'), '5 Series'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'BMW'), 'X5'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Mercedes-Benz'), 'C-Class'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Mercedes-Benz'), 'E-Class'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Mercedes-Benz'), 'GLC'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Chevrolet'), 'Malibu'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Chevrolet'), 'Impala'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Chevrolet'), 'Tahoe'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Nissan'), 'Altima'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Nissan'), 'Rogue'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Nissan'), 'Sentra'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Hyundai'), 'Elantra'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Hyundai'), 'Sonata'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Hyundai'), 'Tucson'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Kia'), 'Soul'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Kia'), 'Sportage'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Kia'), 'Sorento'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Peugeot'), '208'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Peugeot'), '3008'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Peugeot'), '5008'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Fiat'), '500'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Fiat'), 'Panda'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Fiat'), 'Tipo'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Renault'), 'Clio'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Renault'), 'Megane'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Renault'), 'Captur'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Audi'), 'A3'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Audi'), 'Q5'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Audi'), 'A6'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Mazda'), 'Mazda3'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Mazda'), 'CX-5'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Mazda'), 'MX-5'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Subaru'), 'Impreza'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Subaru'), 'Outback'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Subaru'), 'Forester'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Volvo'), 'XC40'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Volvo'), 'S60'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Volvo'), 'V90'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Jeep'), 'Wrangler'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Jeep'), 'Cherokee'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Jeep'), 'Compass'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Jaguar'), 'XE'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Jaguar'), 'F-Pace'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Jaguar'), 'I-Pace'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Land Rover'), 'Range Rover'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Land Rover'), 'Discovery'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Land Rover'), 'Defender'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Skoda'), 'Octavia'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Skoda'), 'Superb'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Skoda'), 'Kodiaq'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Seat'), 'Ibiza'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Seat'), 'Leon'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Seat'), 'Ateca'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Tesla'), 'Model S'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Tesla'), 'Model 3'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Tesla'), 'Model X'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Suzuki'), 'Swift'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Suzuki'), 'Vitara'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Suzuki'), 'Jimny'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Dodge'), 'Challenger'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Dodge'), 'Charger'),
    (gen_random_uuid(), (SELECT "Id" FROM "Manafacturers" WHERE "Name" = 'Dodge'), 'Durango');
    
INSERT INTO "Users"("Id", "FirstName", "LastName", "Email", "Password", "CreatedAt")
VALUES 
    (gen_random_uuid(),'Ivo','Ivić','ivic@gmail.com','AQAAAAIAAYagAAAAEJlnBPBjrD04lOisfvjs9vB5g/7/z/4lZuADlSKB6QmDTKEIou8LQz8WHOmpEXR5LQ==','2024-12-12 23:44:58.658571');

INSERT INTO "Locations"("LocationId", "Latitude", "Longitude", "Adress", "CreatedAt", "UpdatedAt")
VALUES
    (gen_random_uuid(), 46.30444, 16.33778, 'Varaždin', '2024-12-12 23:44:58.658571', '2024-12-12 23:44:58.658571');

INSERT INTO "CarListings"("Id", "ModelId", "YearOfProduction", "NumberOfSeats", "FuelType", "RentalPriceType", "LocationId", "NumberOfKilometers", "RegistrationNumber", "Description", "IsActive", "UserId")
VALUES
    ('0193bd27-e760-7ff0-90eb-3b80f8759259', (SELECT "Id" FROM "Models" WHERE "Name" = 'Charger'), 2012, 4, 'Petrol', 12.0, (SELECT "LocationId" FROM "Locations" WHERE "Adress" = 'Varaždin'), 150000, 'VŽ-502-LT', 'DOŽ', true, (SELECT "Id" FROM "Users" WHERE "Email"='ivic@gmail.com'));

INSERT INTO "CarListingPictures"("Id", "CarListingId", "Image")
VALUES
    (gen_random_uuid(), '0193bd27-e760-7ff0-90eb-3b80f8759259', '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');


    INSERT INTO "CarReservations" (
    "Id", "CarListingId", "UserId", "StartDate", "EndDate", "TotalPrice", "Status", "IsPaid", "CreatedAt"
) VALUES 
(
    '01945546-8c20-7d40-a0a7-0636e796c03c', 
    '0193bd27-e760-7ff0-90eb-3b80f8759259', 
   (SELECT "Id" FROM "Users" WHERE "Email"='ivic@gmail.com'), 
    '2025-01-12T10:00:00', 
    '2025-01-16T10:00:00', 
    60.0, 
    1, 
    false, 
    '2024-12-11T12:00:00'
),
(
    '019456bc-358c-7107-a2a4-4e70939c0d80', 
    '0193bd27-e760-7ff0-90eb-3b80f8759259', 
   (SELECT "Id" FROM "Users" WHERE "Email"='ivic@gmail.com'), 
    '2025-02-05T09:30:00', 
    '2025-02-10T09:30:00', 
    75.0, 
    1, 
    true, 
    '2024-12-04T14:15:00'
),
(
    '019456bc-358c-7107-a2a4-4e70939c0d81', 
    '0193bd27-e760-7ff0-90eb-3b80f8759259', 
   (SELECT "Id" FROM "Users" WHERE "Email"='ivic@gmail.com'), 
    '2025-02-05T09:30:00', 
    '2025-02-10T09:30:00', 
    75.0, 
    1, 
    true, 
    '2025-01-04T14:15:00'
);

