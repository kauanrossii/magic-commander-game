CREATE TABLE public."User" (
	"Id" int4 GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	"Key" uuid NOT NULL,
	"Name" varchar(100) NOT NULL,
	"Email" varchar(100) NULL,
	"Password" varchar(100) NOT NULL,
	"CreatedAt" timestamptz NOT NULL,
	"UpdatedAt" timestamptz NOT NULL,
	"Active" BOOLEAN DEFAULT true NOT NULL,
	CONSTRAINT "PK_User" PRIMARY KEY ("Id"),
	CONSTRAINT "UN_UserKey" UNIQUE ("Key")
);

CREATE TABLE public."Deck" (
	"Id" int4 GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	"Key" uuid NOT NULL,
	"Name" varchar(100) NOT NULL,
	"CommanderId" int4 NOT NULL,
	"CreatedAt" timestamptz NOT NULL,
	"UpdatedAt" timestamptz NOT NULL,
	CONSTRAINT "PK_Deck" PRIMARY KEY ("Id"),
	CONSTRAINT "UN_DeckKey" UNIQUE ("Key"),
	CONSTRAINT "FK_Deck_Card_Id" FOREIGN KEY ("CommanderId") REFERENCES public."DeckCard"("Id")
);

CREATE TABLE public."DeckCard" (
	"Id" int4 GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	"Key" uuid NOT NULL,
	"Name" varchar(100) NOT NULL,
	"CardId" int4 NOT NULL,
    "DeckId" int4 NOT NULL,
    "UserId" int4 NOT NULL,
	"CreatedAt" timestamptz NOT NULL,
	"UpdatedAt" timestamptz NOT NULL,
	CONSTRAINT "PK_DeckCard" PRIMARY KEY ("Id"),
	CONSTRAINT "UN_DeckCardKey" UNIQUE ("Key"),
    CONSTRAINT "FK_DeckCard_Deck_Id" FOREIGN KEY ("DeckId") REFERENCES public."Deck"("Id"),
    CONSTRAINT "FK_DeckCard_Card_Id" FOREIGN KEY ("CardId") REFERENCES public."Card"("Id"),
    CONSTRAINT "FK_DeckCard_User_Id" FOREIGN KEY ("UserId") REFERENCES public."User"("Id")
);

CREATE TABLE public."Card" (
	"Id" int4 GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	"Key" uuid NOT NULL,
	"MultiverseId" int4 NOT NULL,
	"Cmc" int4 NOT NULL,
	"Name" varchar(150) NOT NULL,
	"ManaCost" varchar(10) NOT NULL,
	"Set" varchar(100) NOT NULL,
	"Text" varchar(100) NOT NULL,
	"Name" varchar(100) NOT NULL,
	"Name" varchar(100) NOT NULL,
	"CreatedAt" timestamptz NOT NULL,
	"UpdatedAt" timestamptz NOT NULL,
	CONSTRAINT "PK_Deck" PRIMARY KEY ("Id"),
	CONSTRAINT "UN_DeckKey" UNIQUE ("Key"),
	CONSTRAINT "FK_Deck_Card_Id" FOREIGN KEY ("CommanderId") REFERENCES public."DeckCard"("Id")
);