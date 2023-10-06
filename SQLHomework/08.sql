--test veritabanınızda employee isimli sütun bilgileri id(INTEGER), name VARCHAR(50), birthday DATE, email VARCHAR(100) olan bir tablo oluşturalım.
create table employee
(
	id serial primary key,
	name varchar(50) not null,
	birthday date not null,
	email varchar(100)
)

--Oluşturduğumuz employee tablosuna 'Mockaroo' servisini kullanarak 50 adet veri ekleyelim.
insert into employee (id, name, birthday, email) values (1, 'Marcello', '1960-09-05 17:18:25', 'mironmonger0@bravesites.com');
insert into employee (id, name, birthday, email) values (2, 'Ethelred', '1972-05-15 04:35:57', 'edockwra1@hostgator.com');
insert into employee (id, name, birthday, email) values (3, 'Ronna', '1983-09-03 13:45:41', 'rhamlyn2@rediff.com');
insert into employee (id, name, birthday, email) values (4, 'Violante', '1980-07-04 14:17:22', 'vpahl3@bloglines.com');
insert into employee (id, name, birthday, email) values (5, 'Oliviero', '1964-02-02 06:40:49', 'ohanselmann4@youtube.com');
insert into employee (id, name, birthday, email) values (6, 'Fancie', '1987-12-02 21:22:11', 'fvanvuuren5@chron.com');
insert into employee (id, name, birthday, email) values (7, 'Elita', '1950-02-21 14:32:18', 'epeete6@netlog.com');
insert into employee (id, name, birthday, email) values (8, 'Nicolai', '2012-12-29 06:20:56', 'nlaffin7@mysql.com');
insert into employee (id, name, birthday, email) values (9, 'Avery', '1962-12-07 07:41:26', 'aspackman8@admin.ch');
insert into employee (id, name, birthday, email) values (10, 'Bebe', '2016-02-20 12:02:55', 'bjobern9@google.cn');
insert into employee (id, name, birthday, email) values (11, 'Koralle', '1968-12-05 10:21:17', 'ktomaselloa@diigo.com');
insert into employee (id, name, birthday, email) values (12, 'Stevy', '1991-01-15 13:31:49', 'smadeleyb@mit.edu');
insert into employee (id, name, birthday, email) values (13, 'Leontyne', '1986-09-08 17:15:06', 'ltebbutc@usda.gov');
insert into employee (id, name, birthday, email) values (14, 'Eamon', '1982-10-21 07:39:42', 'emcpartlind@omniture.com');
insert into employee (id, name, birthday, email) values (15, 'Faun', '1964-03-11 12:44:50', 'fcarwithane@nytimes.com');
insert into employee (id, name, birthday, email) values (16, 'Man', '1970-11-02 14:26:46', 'mpepineauxf@quantcast.com');
insert into employee (id, name, birthday, email) values (17, 'Teriann', '1960-10-17 22:00:37', 'tshimukg@redcross.org');
insert into employee (id, name, birthday, email) values (18, 'Olivette', '2005-09-19 01:44:28', 'omacpikeh@jiathis.com');
insert into employee (id, name, birthday, email) values (19, 'Aubrette', '1968-11-05 03:05:11', 'adotsoni@amazon.co.jp');
insert into employee (id, name, birthday, email) values (20, 'Jane', '2009-02-17 19:34:08', 'jpancastj@wordpress.org');
insert into employee (id, name, birthday, email) values (21, 'Maurita', '1986-09-08 15:30:06', 'mbarwisek@cargocollective.com');
insert into employee (id, name, birthday, email) values (22, 'Aldin', '1973-08-24 19:03:55', 'arobyl@imgur.com');
insert into employee (id, name, birthday, email) values (23, 'Amaleta', '2011-10-20 11:58:05', 'ashafierm@census.gov');
insert into employee (id, name, birthday, email) values (24, 'Bryna', '1990-10-22 22:46:24', 'bsadryn@blogspot.com');
insert into employee (id, name, birthday, email) values (25, 'Tonya', '1967-08-25 02:55:25', 'tloinso@bloomberg.com');
insert into employee (id, name, birthday, email) values (26, 'Riccardo', '2014-05-13 08:23:02', 'rneathwayp@cnet.com');
insert into employee (id, name, birthday, email) values (27, 'Langsdon', '1968-12-14 10:55:26', 'lpawlickiq@zimbio.com');
insert into employee (id, name, birthday, email) values (28, 'Rici', '1961-07-04 19:01:49', 'rbourdisr@sitemeter.com');
insert into employee (id, name, birthday, email) values (29, 'Germaine', '1951-01-14 04:35:42', 'ghazeldenes@dyndns.org');
insert into employee (id, name, birthday, email) values (30, 'Earvin', '2004-05-13 23:45:53', 'evidot@tiny.cc');
insert into employee (id, name, birthday, email) values (31, 'Eddie', '1993-06-15 07:05:49', 'eeglaiseu@typepad.com');
insert into employee (id, name, birthday, email) values (32, 'Clemente', '2011-04-30 02:15:44', 'cstreeterv@tuttocitta.it');
insert into employee (id, name, birthday, email) values (33, 'Sky', '2015-02-12 13:17:43', 'srapiw@lulu.com');
insert into employee (id, name, birthday, email) values (34, 'Eddy', '2017-11-25 22:00:00', 'eyelyashevx@slate.com');
insert into employee (id, name, birthday, email) values (35, 'Cybill', '2002-03-25 05:41:18', 'cbrithmany@is.gd');
insert into employee (id, name, birthday, email) values (36, 'Murial', '1956-01-31 15:12:13', 'mleighfieldz@simplemachines.org');
insert into employee (id, name, birthday, email) values (37, 'Omero', '1962-05-19 20:14:28', 'ospellard10@sohu.com');
insert into employee (id, name, birthday, email) values (38, 'Morgen', '1966-12-22 13:25:19', 'mcrielly11@1und1.de');
insert into employee (id, name, birthday, email) values (39, 'Ravi', '1996-11-06 03:27:33', 'rmancer12@friendfeed.com');
insert into employee (id, name, birthday, email) values (40, 'Anestassia', '1978-03-31 09:20:18', 'asauvan13@spotify.com');
insert into employee (id, name, birthday, email) values (41, 'Ulrikaumeko', '1964-09-15 12:18:10', 'usprulls14@narod.ru');
insert into employee (id, name, birthday, email) values (42, 'Shel', '1956-12-29 05:11:18', 'sbraunston15@spotify.com');
insert into employee (id, name, birthday, email) values (43, 'Romola', '1956-04-07 17:16:30', 'rcaught16@upenn.edu');
insert into employee (id, name, birthday, email) values (44, 'Bethena', '1960-10-28 06:16:21', 'btinkler17@ning.com');
insert into employee (id, name, birthday, email) values (45, 'Dulci', '2007-03-19 10:20:37', 'dglazier18@dyndns.org');
insert into employee (id, name, birthday, email) values (46, 'Kelsey', '1984-11-29 11:17:04', 'kmarusic19@businessweek.com');
insert into employee (id, name, birthday, email) values (47, 'Cally', '1978-08-17 04:26:10', 'cdarlington1a@dot.gov');
insert into employee (id, name, birthday, email) values (48, 'Livy', '1992-11-26 03:45:39', 'lpilbeam1b@etsy.com');
insert into employee (id, name, birthday, email) values (49, 'Codi', '1979-04-03 21:09:57', 'ctomashov1c@symantec.com');
insert into employee (id, name, birthday, email) values (50, 'Trent', '2011-08-01 20:19:36', 'tfrangleton1d@usda.gov');


--Sütunların her birine göre diğer sütunları güncelleyecek 5 adet UPDATE işlemi yapalım.
UPDATE employee 
SET name = 'Furkan',
	birthday = '1996-12-20',
    email = 'frknertnt@gmail.com'
WHERE id =22

UPDATE employee
SET name = 'Namık',
    birthday = '1954-02-27',
    email = 'namıkemal9@gmail.com'
WHERE id = 32

UPDATE employee
SET name = 'Reşat Nuri',
    birthday = '1981-04-05',
    email = 'resatnuriguntekin@outlook.com'
WHERE id = 24

UPDATE employee
SET name = 'Recaizade',
    birthday = '1974-06-15',
    email = 'recaizade@mahmudekrem.com'
WHERE id = 3

UPDATE employee
SET name = 'Yakup',
    birthday = '1943-11-11',
    email = 'yakubkadri@karaosmanoglu.com'
WHERE id = 41

--Sütunların her birine göre ilgili satırı silecek 5 adet DELETE işlemi yapalım.
DELETE FROM employee WHERE id = 31
DELETE FROM employee WHERE name = 'Nicolai'
DELETE FROM employee WHERE birthday = '1956-01-31'
DELETE FROM employee WHERE name = 'Earvin'
DELETE FROM employee WHERE id in(2,4,6,8,10)