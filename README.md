# GoTravelApplication
Software Project Group 4 Repository

Task Instructions available at this link:
https://docs.google.com/spreadsheets/d/1ijL3BByw5I3kAJGhzWt9b_c3nhQ_oEjv5FLoVlgB-Og/edit?usp=sharing

## Make sure you have these

**PLEASE BOOT THIS ON VISUAL STUDIO 2019 AND MAKE SURE YOU HAVE "ASP.NET AND web devolopment" WORKLOAD INSTALLED VIA THE VISUAL STUDIO INSTALLER OR ELSE IT WILL NOT BOOT

THE DATABASE IS ON AWS SO MAKE SURE YOU HAVE AWS TOOLKIT FOR VISUAL STUDIO 2019 INSTALLED AND REMEMBER TO USE THE CREDENTIALS IN THE CSV FILE IN ORDER TO ACCESS THE DATABASE!**

Please be mindful of the Low Fidelity Protoype when doing any work

## Database Diagram:
![Database Diagram](https://user-images.githubusercontent.com/90715801/203161650-de95c548-326f-4f54-a077-343bb5a6d1ef.jpeg)
Use this diagram to give youself an understanding of the schema

### I will explain the purpose of some of the more ambiguous tables here

#### CustomerBookings
This table stores all records of purchased bookings.
- Customers will have one record in CustomerBookings for each Booking they own

#### CartBookings
This table has one record for all items in a customers cart, when a customer checks out, all his records will be deleted from the CartBookings table and a CustomerBooking record will be made for each of the purchased bookings
- Customers will have one record in CartBookings for each Booking in their cart

#### Pictures
### I am removing this table so do not worry about it

#### ReceptionistChanges
This table gets a record each time a receptionist makes a change to the status of a customer booking

#### ModRequest
This table gets a record each time a Moderator makes a request to an admin

#### AdminResponse
This table gets a record each time a admin responds to an moderator request
