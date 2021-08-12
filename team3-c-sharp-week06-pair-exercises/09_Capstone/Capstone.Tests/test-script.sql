-- Delete all of the data
DELETE FROM reservation;
DELETE FROM space;
DELETE FROM category_venue;
DELETE FROM category;
DELETE FROM venue;
DELETE FROM city;
DELETE FROM state;

--Insert a fake state

INSERT INTO state (abbreviation, name)
VALUES ('OH', 'Ohio');

-- Insert a fake city
SET IDENTITY_INSERT city ON
INSERT INTO city (id, name, state_abbreviation)
VALUES (1, 'CharlieVille', 'OH');
--DECLARE @newCityId int = (SELECT @@IDENTITY);
SET IDENTITY_INSERT city OFF

--Insert a fake venue
SET IDENTITY_INSERT venue ON
INSERT INTO venue (id, name, city_id, description)
VALUES (1, 'TechElevator', 1, 'A cool place to learn C#');
--DECLARE @newVenueId int = (SELECT @@IDENTITY);
SET IDENTITY_INSERT venue OFF

--Insert a fake space
SET IDENTITY_INSERT space ON
INSERT INTO space (id, venue_id, name, is_accessible, open_from, open_to, daily_rate, max_occupancy)
VALUES (1, 1, 'Computer Club', 1, 1, 11, 500.00, 1000);
--DECLARE @newSpaceId int = (SELECT @@IDENTITY);
SET IDENTITY_INSERT space OFF

-- Insert a fake reservation
SET IDENTITY_INSERT reservation ON
INSERT INTO reservation (reservation_id, space_id, number_of_attendees, start_date, end_date, reserved_for)
VALUES (1, 1, 10, '5/5/2022', '5/10/2022', 'Charlie');
--DECLARE @newReservationId int = (SELECT @@IDENTITY);
SET IDENTITY_INSERT reservation OFF

-- Insert a fake category
SET IDENTITY_INSERT category ON
INSERT INTO category (id, name)
VALUES (1, 'Party');
--DECLARE @newCategoryId int = (SELECT @@IDENTITY);
SET IDENTITY_INSERT category OFF

--Insert a fake category_venue
INSERT INTO category_venue (venue_id, category_id)
VALUES (1, 1);




