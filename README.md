# Filmstudion
Dynamiska Webbsystem 2 - mars 2021
## Inlämningsuppgift i REST och anrop från användare av API:er
1.  43/100 (komplettering)
2.  100/100 (VG) 
### Senario
I detta påhittade uppdrag så ska du skapa en webbapplikation riktad till föreningar som är anslutna till Sveriges Förenade Filmstudios (Länkar till en externa sida.) (SFF) där man via ett API och klientgränssnitt kan boka/beställa filmer till sin förening.

SFF fungerar så att lokala filmintresserade bildar föreningar (en filmstudio), dessa föreningar ingår i medlemskap hos SFF som är förbund för alla filmstudios i Sverige. SFF köper rättigheter från filmdistributörerer att låna ett visst antal exemplar av olika filmer, som SFF sen skickar till lokala föreningar. Filmstudion visar sedan dem på på exempelvis kulturhus och mindre biografer runt om i landet.

Förut skedde detta via blanketter, och filmerna man kunde låna fraktades som stora filmrullar, varför filmer bara kunde visas samtidigt på ett begränsat antal ställen i taget. I dag skickas, och visas, filmerna digitalt - men avtalen ser fortfarande likadana ut; så SFF måste begränsa hur många filmstudios som samtidigt kan visa en viss film!
________________________________________________________________________________________________________________________________________________________________________________
http://localhost:6600/api/v1
# Åtkomstpunkter
## PUBLIC
### /public/filmStudios
    # GET 
    hämtar alla FilmStudios
### /public/filmStudios/{filmStudioId}
    # GET 
    hämtar en specifik FilmSudio
### /public/movies
    # GET 
    hämtar alla Movies
### /public/movies/{moveId}
    # GET 
    hämtar en specifik Movie

## USER
### /users/register
    # POST
    För att registrera ny filmstudio-användare, body:
    {
        "name": "string",
        "location": "string",
        "chairpersonName": "string",
        "email": "email",
        "password": "string"
    }
### /user/register/admin
    #POST
    För att registrera administratör
    {
        "email": "email",
        "password": "string"
    }
    
### /users/authenticate
    # POST
    För att autesiera användare , body:
    {
        "email": "string",
        "password": "string"
    }

## AUTENSIERADE ANVÄNDARE
### /filmcopies/{moveId}
    # GET 
    hämtar en specifik Movie inklusive antalet lediga exemplar
    
### /filmcopies
    # GET 
    Hämtar utlånade FilmCopies, Admin = alla, Filmstudio = sina egna lån
    
###  /movies
    # GET
    hämtar alla Movies inklusive antalet tillgängliga filmCopies
    
###  /movies
    # POST 
    Lägger till ny Movie - FilmCopies genereas automatiskt, body: 
    {
        "title": "string",
        "releaseYear": 0,
        "country": "string",
        "director": "string",
        "amountOfCopies": 0
    }

### /movies/{moveId}
    # PUT 
    uppdaterar information, om AmountOfCopies ändras - FilmCopies genereas/elimeneras automatiskt, body:
    {
        "title": "string",
        "releaseYear": 0,
        "country": "string",
        "director": "string",
        "amountOfCopies": 0
    }

### /FilmCopies/{movieId}/rent
    # PUT 
    hyr Movie
    
### /FilmCopies/{movieId}/return
    # PUT 
    lämna tillbaka Movie
_______________________________________________________________________________________________________________________________________________________________________________

### Kravlista:
* Det inlämande git-repot ska innehålla ett Webb-API skapats med ASP.NET och som går att starta med .NET Core 3 eller .NET 5.
* API:et ska tillhandahålla resursena "film" och "filmstudio"
* Resursen film innehåller namn, utgivningsår, land och regisör"
* Resursen filmstudio innehåller namn, ort och namnet samt kontaktuppgifterna till föreningens ordförand e"
* En filmstudio ska kunna registrera sig via API:et
* En administratör ska kunna registrera sig via API:et, en administratör är inte en filmstudio
* Både filmstudios och administratörer ska kunna autentisera sig via API:et
* Autentisering till API:et ska vara implementerat med lämplig lösning för ett REST-baserat Webb-API
* Det ska gå att lägga till en nya filmer via Webb-API:et om man är autentiserad som administratör
* Det ska gå att ändra informationen om en film via Webb-API:et om man är autentiserad som administratör
* En autentiserad administratör ska kunna ändra antalet tillgängliga exemplar som går att låna av varje film
* En autentiserad filmstudio ska kunna låna ett exemplar av en film via Webb-API:et om det finns exemplar tillgängliga
* En autentiserad filmstudio ska kunna lämna tillbaka ett lånat exempelar av en film via Webb-AP:et
* Endast samma filmstudio som lånat en film ska kunna lämna tillbaka sitt exemplar
* Via Webb-API:et ska alla filmer kunna hämtas
* Via Webb-API:et ska informationen om en enskild film kunna hämtas
* Endast autentiserade filmstudios och administratörer ska kunna se antalet tillgänga exemplar av filmer via Webb-API:et
* Via Webb-API:et ska alla filmstudios kunna hämtas
* Via Webb-API:et ska informationen om en enskild filmstudio kunna hämtas
* En autentiserad filmstudio ska kunna via Webb-API:et kunna hämta vilka filmer som studion för närvarande har lånat
* En autentiserade administratörer ska kunna se vilka filmer samtliga filmstudios har lånat
* Det inlämnade git-repot ska innehålla ett fungerande kliengränsnitt för att interagera med Webb-API:et.
* Klientgränssnittet tillåter åtkomst till API:et för filmstudios att registera sig, logga-in samt logga-ut
* En filmstudio som är inloggad kan i klientgränssnittet se alla tillgängliga filmer från API:et
* En filmstudio som är inloggad kan tydligt se sina lånade filmer i klientgränssnittet
* En filmstudio som är inloggad kan låna ett exemplar av en film
* En filmstudio som är inloggad kan lämna tillbaka ett lånat exemplar av en film
* Det inlämnade git-repot innehåller en fil med namnet "readme.md" där du beskrivit vilka åtkomstpunkter som finns i API:et och hur de används
* Det inlämande git-repot innehåller en fil med namnet "reflections" i formatet md, txt eller pdf
* I reflections-filen under rubkriken "REST" har du förklarat och motiverat hur du designat dina åtkomstpunker och resurser för att uppfylla kraven på Webb-API:et
* I reflections-filen under rubkriken "Implementation" jämför du och motiverar vilka interna modeller som finns och om/hur de skilljer sig från de synliga resuserna i Webb-API:et
* I reflections-filen under rubkriken "Säkerhet" har du motiverat dina säkerhetsåtgärder i applikation. Svara på hur har du kontrollerat och begränsat vilken information som
finns tillgänglig vid anrop till API:et? samt hur inloggning och utloggning fungerar i klientgränsnittet.
* I reflections-filen under rubkriken "Klientgränsnitt" har du motiverat hur du tänkt kring användarbarheten i din klientapplikation utifrån ovanstående kravlista, vad var
viktigast att få med? Anser du att gränssnittet är användarvänligt?
