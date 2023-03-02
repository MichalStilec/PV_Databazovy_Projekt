create table Materialy(
id int primary key identity(1,1),
nazev varchar(30) NOT NULL,
typ varchar(30)
);

create table Vyrobek(
id int primary key identity(1,1),
nazev varchar(30) NOT NULL,
cena float NOT NULL,
datum_vyroby date NOT NULL,
id_materialu int foreign key references Materialy(id)
);

create table Sklady(
id int primary key identity(1,1),
nazev varchar(30) NOT NULL,
adresa varchar(30) NOT NULL,
kapacita int NOT NULL
);

create table Skladove_polozky(
id int primary key identity(1,1),
id_skladu int foreign key references Sklady(id),
id_vyrobku int foreign key references Vyrobek(id),
mnozstvi int NOT NULL
);

create table Objednavky(
id int primary key identity(1,1),
id_skladu int foreign key references Sklady(id),
prijemce varchar(30) NOT NULL,
adresa_prijemce varchar(30) NOT NULL,
datum datetime NOT NULL
);

insert into Materialy(nazev,typ) values('drevo', 'dubove');
select * from Materialy;
DELETE FROM Materialy WHERE id = 2;

insert into Vyrobek(nazev,cena,datum_vyroby,id_materialu) values('zidle', 500, '2022-06-01', 1);
select Vyrobek.nazev, cena, datum_vyroby, Materialy.nazev from Vyrobek
inner join Materialy on Materialy.id = Vyrobek.id_materialu;


insert into Sklady(nazev,adresa,kapacita) values('Ikea', 'Nejaka', 300);
select * from Sklady;

DELETE FROM Sklady WHERE id = 2;

insert into Skladove_polozky(id_skladu,id_vyrobku,mnozstvi) values(1,1,50);
select Skladove_polozky.id, Sklady.nazev, Vyrobek.nazev from Skladove_polozky
inner join Sklady on Skladove_polozky.id_skladu = Sklady.id
inner join Vyrobek on Skladove_polozky.id_vyrobku = Vyrobek.id;

insert into Objednavky(id_skladu,prijemce,adresa_prijemce,datum) values(1, 'Nuts', 'Deezova', '2023-12-12 14:30:00');
select Sklady.nazev as 'nazev skladu', Sklady.adresa as 'adresa skladu', prijemce, adresa_prijemce, datum from Objednavky
inner join Sklady on Objednavky.id_skladu = Sklady.id;

UPDATE Vyrobek SET cena = cena * 2 WHERE id = 1;

