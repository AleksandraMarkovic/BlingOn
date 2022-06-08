# blingOn
## Theme - Ring store

The project was conceived as a ring store. Each ring must have its own brand and size (it can have one or more sizes). Logged-in users can place an order and order products in the desired sizes and quantities.



## Functionalities

The functionalities in the project were done based on the role of the user. 
**Unauthorized users** can search for products and brands (only active ones will be retrieved, i.e. those with DeleteAt = null) and register.
**Authorized users** can order a product, rate it from 1 to 5, cancel / delete their orders and update and delete their account.
**Admin** can insert, update and delete records from all tables. Only he can search the Users table. Only delivery date can be updated in the order table (DeliveredAt column).
The login was done with a token that has a limited duration (set to 120 seconds, can be changed).
There is also a UseCaseLogs table which stores which user executed which command and admin can search this table.



## Login

Admin: markovic@gmail.com (email), sifra123 (password)

Korisnik: jelena@gmail.com (email), sifra123 (password)



## Project structure

The solution is divided into multiple layers: Domain, DataAccess, Application, Implementation and API. 


## Database diagram

![alt text](https://user-images.githubusercontent.com/51022026/122390290-1de99200-cf72-11eb-8871-458be77271d4.png)
