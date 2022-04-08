create table Agente(
Nome varchar(20) not null,
Cognome varchar(20) not null,
CodiceFiscale varchar(16) primary key not null,
AreaGeografica varchar(40) not null,
AnnoDiInizioAttivita int not null
)