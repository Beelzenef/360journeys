create database if not exists talessya;
use talessya;

drop table if exists reino;
drop table if exists ciudad;
drop table if exists gobernante;

create table gobernante
(
	id int,
	nombre varchar(70),
		primary key (id)
);

create table ciudad
(
	id int,
	nombre varchar(70),
		primary key (id)
);

create table reino
(
	id int,
	nombre varchar(70),
	capital int,
	gobernante int,
		primary key (id),
		foreign key (capital) references ciudad (id)
			on update cascade
			on delete restrict,
		foreing key (gobernante) references gobernante (id)
			on update cascade
			on delete restrict
);