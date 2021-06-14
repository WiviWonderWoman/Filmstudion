# Filmstudion
## Syfte 
Inlämningsuppgift i kursen Dynamiska Webbsystem 2, Webbutvecklare inom .NET, YH-utbildning – mars 2021
1.  43/100 (komplettering)
2.  100/100 (VG) 
## Tekniker
* ASP.NET Core 
* Entity Framework Core
* ASP.NET Core Identity
* Swagger UI
* JSON Web Tokens
* RESTful API
* HTML
* JavaScript
* CSS
## Läranderesultaten
I denna uppgift berörs framför allt läranderesultaten:
Kunskaper:
* redogöra för de vanligaste databastyperna och deras styrkor, svagheter och lämpliga användningsområden
* beskriva de grundläggande principerna i REST-arkitektur vid skapande av API:er
Färdigheter:
* implementera ett webbaserat API i ASP.NET samt använda HTML5, CSS3 och Javascript för dess presentation för och interaktion med slutanvändare
* tillämpa metoder för att hantera tillstånd och dess överföring mellan klient och server i en applikation där servern har ett webbaserat API
* begränsa och kontrollera en användares behörighet i en applikation
Kompetenser:
* självständigt och i grupp analysera användarupplevelsen av en applikations gränssnitt
________________________________________________________________________________________________________________________________________________________________________________
# Användning
http://localhost:6600/api/v1
## Åtkomstpunkter
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
