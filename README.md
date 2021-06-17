# blingOn
Tema projekta - Prodavnica prstenja

Projekat je odrađen kao prodavnica prstenja. Svaki prsten mora imati svoj brend i veličinu koju može imati jednu ili više. Ulogovani korisnici mogu napraviti porudžbinu i poručiti proizvode u željenim veličinama i količinama.



Funkcionalnosti

Funkcionalnosti na sajtu odrađene su na osnovu uloge korisnika. 

Neautorizovani korisnici mogu pretraživati proizvode (dohvataće se samo aktivni tj. oni kojima je DeleteAt = null) i registrovati se.

Ulogovani korisnici mogu poručiti proizvod, oceniti ocenom 1 do 5, otkazati/obrisati svoje porudžbine i ažurirati i obrisati svoj nalog.

Admin može vršiti insert, update i delete svih tabela. U order tabeli može ažurirati samo datum dostave (kolona DeliveredAt).

Logovanje je odrađeno sa tokenom koji ima ograničeno trajanje (podešen je na 120 sekundi).



Logovanje

Admin: markovic@gmail.com, sifra123

Korisnik: jelena@gmail.com sifra123



Struktura projekta

Projekat je izdeljen u zasebne celine: Domenski sloj, DataAccess sloj, Application sloj, Implementation sloj i API. 
