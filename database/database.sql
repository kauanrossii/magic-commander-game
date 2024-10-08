CREATE TABLE "User" (
	"Id" INTEGER NOT NULL UNIQUE,
	"Username" VARCHAR,
	"Email" VARCHAR,
	"Password" VARCHAR,
	"CreatedAt" DATE,
	"Active" BOOLEAN,
	PRIMARY KEY("Id")
);


CREATE TABLE "Import" (
	"Id" INTEGER NOT NULL UNIQUE,
	"UserId" INTEGER,
	"CreatedAt" DATE,
	PRIMARY KEY("Id")
);


CREATE TABLE "ImportStatus" (
	"Id" INTEGER NOT NULL UNIQUE,
	"ImportId" INTEGER,
	"Type" INTEGER,
	"CreatedAt" DATE,
	PRIMARY KEY("Id")
);


CREATE TABLE "Deck" (
	"Id" INTEGER NOT NULL UNIQUE,
	"ImportId" INTEGER,
	PRIMARY KEY("Id")
);


CREATE TABLE "Card" (
	"Id" INTEGER NOT NULL UNIQUE,
	"Names" VARCHAR,
	"ManaCost" VARCHAR,
	"Variations" ,
	PRIMARY KEY("Id")
);


CREATE TABLE "ImportCard" (
	"Id" INTEGER NOT NULL UNIQUE,
	"Status" INTEGER,
	"Data" JSON,
	PRIMARY KEY("Id")
);


ALTER TABLE "ImportStatus"
ADD FOREIGN KEY("ImportId") REFERENCES "Import"("Id")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "Import"
ADD FOREIGN KEY("UserId") REFERENCES "User"("Id")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "Deck"
ADD FOREIGN KEY("ImportId") REFERENCES "Import"("Id")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "Card"
ADD FOREIGN KEY("Id") REFERENCES "Deck"("Id")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "ImportCard"
ADD FOREIGN KEY("Id") REFERENCES "Import"("Id")
ON UPDATE NO ACTION ON DELETE NO ACTION;