# blingOn
## Tema projekta - Prodavnica prstenja

Projekat je odrađen kao prodavnica prstenja. Svaki prsten mora imati svoj brend i veličinu koju može imati jednu ili više. Ulogovani korisnici mogu napraviti porudžbinu i poručiti proizvode u željenim veličinama i količinama.



## Funkcionalnosti

Funkcionalnosti u projektu odrađene su na osnovu uloge korisnika. 
**Neautorizovani korisnici** mogu pretraživati proizvode i brendove (dohvataće se samo aktivni tj. oni kojima je DeleteAt = null) i registrovati se.
**Ulogovani korisnici** mogu poručiti proizvod, oceniti ocenom 1 do 5, otkazati/obrisati svoje porudžbine i ažurirati i obrisati svoj nalog.
**Admin** može vršiti insert, update i delete svih tabela. Tabelu korisnici može samo admin pretraživati. U order tabeli može ažurirati samo datum dostave (kolona DeliveredAt).
Logovanje je odrađeno sa tokenom koji ima ograničeno trajanje (podešen je na 120 sekundi).
Takođe postoji tabela UseCaseLogs gde se čuva koji korisnik je izvršio koju komandu i admin je može pretraživati.



## Logovanje

Admin: markovic@gmail.com, sifra123

Korisnik: jelena@gmail.com, sifra123



## Struktura projekta

Projekat je izdeljen u zasebne celine: Domenski sloj, DataAccess sloj, Application sloj, Implementation sloj i API. 


## Dijagram baze

![alt text](https://user-images.githubusercontent.com/51022026/122390290-1de99200-cf72-11eb-8871-458be77271d4.png)
