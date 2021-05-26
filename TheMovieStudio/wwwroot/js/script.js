"use strict";

const app = {
    content: document.getElementById('content'),
    logMessage: document.getElementById('logMessage'),
    rentMessage: document.getElementById('rentMessage'),
    regBtn: document.getElementById('reg'),
    logBtn: document.getElementById('login'),
    homeBtn: document.getElementById('home'),
    studioBtn: document.getElementById('studios'),
    movieBtn: document.getElementById('movies'),
    globaltoken: "",
    id: 0
};

app.homeBtn.addEventListener('click', function ()
{
    app.rentMessage.innerHTML = "";

    if (app.globaltoken === "")
    {
        app.logMessage.innerHTML = "";
    }
    
    app.content.innerHTML = `<div id="intro">
                <h1>Välkommen till SFF!</h1>
                <h3>FILMSTUDIOR: </h3>
                <p>Här kan alla se samtliga landets anslutna filmstudior.</p>
                <h3>FILMER: </h3>
                <p>Här kan alla se vilka filmer vi erbjuder.</p>
                <h3>REGISTRERA: </h3>
                <p>En filmstudio måste regisreras innan inloggning.</p>
                <h3>LOGGA IN: </h3>
                <p>Som registrerad filmstudio kan du logga in för att:</p>
                <ul>
                    <li><h4>FILMER: </h4></li>
                    <li>
                        <ul>
                            <li><p>se om filmen är tillgänlig</p></li>
                            <li><p>låna filmer</p></li>
                        </ul>
                    </li>
                    <li><h4>MIN SIDA: </h4></li>
                    <li>
                        <ul>
                            <li><p>se era aktiva lån</p></li>
                            <li><p>lämna tillbaka filmer</p></li>
                        </ul>
                    </li>
                </ul>
            </div>`;
});

app.movieBtn.addEventListener('click', function getMovies() {
    app.rentMessage.innerHTML = "";

    if (app.globaltoken === "") {
        fetch('/api/v1/public/movies/')
            .then(resp => resp.json())
            .then(movies => showMovies(movies))
    }
    else {
        fetch('/api/v1/movies', {
            method: 'GET',
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
                'Authorization': `Bearer ${app.globaltoken}`
            }
        })
            .then(resp => resp.json())
            .then(movies => showMovies(movies))
    }
});

async function showMovies(movies)
{
    if (app.globaltoken === "")
    {
        app.logMessage.innerHTML = "";
    }

    app.content.innerHTML = `<div id="moviesDiv"></div>`;
    let moviesDiv = document.getElementById('moviesDiv');

    for (var i = 0; i < movies.length; i++) {

        let movie = movies[i];
        if (app.globaltoken !== "") {
            if (movie.availablefCopies === 0) {
                moviesDiv.innerHTML += `<div id='movie2'><h3>Inga tillgängliga kopior</h3><h4>Film Id: ${movie.movieId}</h4><h4>${movie.title}</h4> 
                <p>Regissör:   ${movie.director}</p>
                <p>Utgiven år:     ${movie.releaseYear}</p>
                <p>Ursprung:       ${movie.country}</p>
                </div>`
            }
            else {
                moviesDiv.innerHTML += `<div id='movie'><h3>Film Id: ${movie.movieId}</h3><h3>${movie.title}</h3> 
                <p>Regissör:   ${movie.director}</p>
                <p>Utgiven år:     ${movie.releaseYear}</p>
                <p>Ursprung:       ${movie.country}</p>
                <p>Tillgängliga kopior: ${movie.availablefCopies}</p></div>`
            }
        }
        else {
            moviesDiv.innerHTML += `<div id='movie'><h3>${movie.title}</h3> 
            <p>Regissör:   ${movie.director}</p>
            <p>Utgiven år:     ${movie.releaseYear}</p>
            <p>Ursprung:       ${movie.country}</p></div>`
        }
    }

    if (app.globaltoken !== "") {
        moviesDiv.innerHTML += `<div id='movie'><h2>Låna</h2>
            <form id="rentForm">
                <div class="form-group">
                    <label for="MovieId">Film Id</label><br />
                    <input type="text" class="form-control" id="movieId">
                </div>
                <div>
                    <br /><button type="submit" class="btn btn-dark" id="rent">RENT</button>
                </div>
            </form></div>`;

        let rentForm = document.getElementById('rentForm');

        rentForm.addEventListener('submit', async (e) => {
            e.preventDefault();

            let id = document.getElementById('movieId').value;

            let response = await fetch('/api/v1/FilmCopies/' + id + '/rent', {
                method: 'PUT',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8',
                    'Authorization': `Bearer ${app.globaltoken}`
                }
            })
            let result = await response.json();
            showRented(result);
        });
    }
}

async function showRented(result) {
    console.log(result);
    app.rentMessage.innerHTML = `<h4 class="message">Ni har nu lånat ${result.movieName}</h4>`;
    getUpdatedMovies();
}

app.studioBtn.addEventListener('click', function ()
{
    fetch('/api/v1/public/filmstudios/')
        .then(resp => resp.json())
        .then(studios => showPublicFilmStudios(studios))
});

async function showPublicFilmStudios(filmStudios)
{
    app.rentMessage.innerHTML = "";

    if (app.globaltoken === "")
    {
        app.logMessage.innerHTML = "";
    }

    app.content.innerHTML = `<div id="studiosDiv"></div>`;
    let studiosDiv = document.getElementById('studiosDiv');

    for (var i = 0; i < filmStudios.length; i++) {
        let studio = filmStudios[i];
        studiosDiv.innerHTML += `<div id="studio"><h3>${studio.name} i ${studio.location}</h3>
                <h4>Ordförande: ${studio.chairpersonName}</h4>
                <h5> ${studio.email}</h5></div>`
    }
}

app.regBtn.addEventListener('click', function () {
    app.rentMessage.innerHTML = "";

    if (app.globaltoken !== "") {
        getUserStudio(app.id)
    }

    if (app.globaltoken === "") {
        app.logMessage.innerHTML = "";

        app.content.innerHTML =
            `<h2>Registrera Filmstudio</h2>
            <form id="registerForm">
                <div class="form-group">
                    <label for="name">Filmstudio</label><br />
                    <input type="text" class="form-control" id="name">
                </div>
                <div class="form-group">
                    <label for="location">Ort</label><br />
                    <input type="text" class="form-control" id="location">
                </div>
                <div class="form-group">
                    <label for="chairPersonName">Ordförande</label><br />
                    <input type="text" class="form-control" id="chairPerson">
                </div>
                <div class="form-group">
                    <label for="email">E-post</label><br />
                    <input type="email" class="form-control" id="email">
                </div>
                <div class="form-group">
                    <label for="password">Lösenord</label><br />
                    <input type="password" class="form-control" id="password"><br />
                </div>
                <div>
                    <br /><button type="submit" class="btn btn-dark" id="submitRegister">Registrera</button>
                </div>
            </form>`

        register();
    }
});

async function register() {

    let registerForm = document.getElementById('registerForm');

    registerForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        let name = document.getElementById('name').value;
        let location = document.getElementById('location').value;
        let chairPerson = document.getElementById('chairPerson').value;
        let email = document.getElementById('email').value;
        let password = document.getElementById('password').value;

        fetch('/users/register', {
            method: 'POST',
            body: JSON.stringify({
                Name: name,
                Location: location,
                ChairpersonName: chairPerson,
                Email: email,
                Password: password
            }),
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
            },
        })
        showLogin()
    });
}

app.logBtn.addEventListener('click', function showLogin() {
    app.rentMessage.innerHTML = "";

    if (app.globaltoken === "") {
        app.logMessage.innerHTML = "";
    }

    app.content.innerHTML =
        `<h2>Logga in</h2>
            <form id="loginForm">
                <div class="form-group">
                    <label for="Email">E-post</label><br />
                    <input type="email" class="form-control" id="email">
                </div>
                <div class="form-group">
                    <label for="Password">Lösenord</label><br />
                    <input type="password" class="form-control" id="password"><br />
                </div>
                <div>
                    <br /><button type="submit" class="btn btn-dark" id="submitLogin">Logga in </button>
                </div>
            </form>`;
    loginUser();
});

async function showLogin() {
    app.rentMessage.innerHTML = "";

    if (app.globaltoken === "") {
        app.logMessage.innerHTML = "";
    }

    app.content.innerHTML =
        `<h2>Logga in</h2>
            <form id="loginForm">
                <div class="form-group">
                    <label for="Email">E-post</label><br />
                    <input type="email" class="form-control" id="email">
                </div>
                <div class="form-group">
                    <label for="Password">Lösenord</label><br />
                    <input type="password" class="form-control" id="password"><br />
                </div>
                <div>
                    <br /><button type="submit" class="btn btn-dark" id="submitLogin">Logga in </button>
                </div>
            </form>`;
    loginUser();
}

async function loginUser() {
    let loginForm = document.getElementById("loginForm");

    loginForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        let email = document.getElementById('email').value;
        let password = document.getElementById('password').value;

        let response = await fetch('/users/authenticate', {
            method: 'POST',
            body: JSON.stringify({
                Email: email,
                Password: password
            }),
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
            },
        })

        let user = await response.json();
        app.globaltoken = user.token;
        app.id = user.id;

        if (app.globaltoken !== undefined) {
            app.regBtn.innerHTML = 'MIN SIDA';
            app.logBtn.innerHTML = 'LOGGA UT';
            getUserStudio(app.id);
        }
        if (app.globaltoken === undefined) {
            app.content.innerHTML = '<h1>Hoppsan, ingen filmstudio registrerad.</h1>';
        }

        app.logBtn.addEventListener('click', function () {

            if (app.globaltoken !== "") {
                app.globaltoken = "";
                app.id = 0;
                app.content.innerHTML = "";
                app.logMessage.innerHTML = `<h2>Du är utloggad</h2><h2>Välkommen åter!</h2>`;

                app.regBtn.innerHTML = 'REGISTRERA';
                app.logBtn.innerHTML = 'LOGGA IN';
            }
        });
    });
}

function getUserStudio(id) {
    fetch('/api/v1/public/filmStudios/' + id)
        .then(resp => resp.json())
        .then(studio => showStudio(studio))
}

function showStudio(studio) {
    getRentedMovies();
    app.logMessage.innerHTML = `<h2>Välkommen <span id="spanEmail">` + studio.email + `</span> du är inloggad för <span id="spanStudio">` + studio.name + `</span></h2>`;
    app.rentMessage.innerHTML = "";
}

function getRentedMovies() {

    fetch('/api/v1/FilmCopies', {
        method: 'GET',
        headers: {
            'Content-type': 'application/json; charset=UTF-8',
            'Authorization': `Bearer ${app.globaltoken}`
        }
    })
        .then(resp => resp.json())
        .then(rented => showRentedMovies(rented))
}

async function showRentedMovies(rented) {

    app.content.innerHTML = `<div id="moviesDiv"></div>`;
    let moviesDiv = document.getElementById('moviesDiv');

    if (rented.length === 0) {
        moviesDiv.innerHTML += `<div><h2>Ni har inga aktiva lån</h2></div>`;
    }

    else if (rented.length >= 1) {
        moviesDiv.innerHTML += `<div><h2>Ni har följande aktiva lån:</h2></div>`;

        for (var i = 0; i < rented.length; i++) {
            let movie = rented[i];
            let id = movie.movieId;
            moviesDiv.innerHTML += `<div id='movie'><h3>${movie.movieName}</h3><p>Film Id: ` + id + `</p></div>`
        }
        moviesDiv.innerHTML += `<div id='returnDiv'><h2>Åtrlämna</h2>
            <form id="returnForm">
                <div class="form-group">
                    <label for="MovieId">Film Id</label><br />
                    <input type="text" class="form-control" id="movieId">
                </div>
                <div>
                    <br /><button type="submit" class="btn btn-dark" id="return">RETURN</button>
                </div>
            </form></div>`;

        let returnForm = document.getElementById('returnForm');

        returnForm.addEventListener('submit', async (e) => {
            e.preventDefault();

            let id = document.getElementById('movieId').value;

            let response = await fetch('/api/v1/FilmCopies/' + id + '/return', {
                method: 'PUT',
                headers: {
                    'Content-type': 'application/json; charset=UTF-8',
                    'Authorization': `Bearer ${app.globaltoken}`
                }
            })

            let result = await response.json();
            showReturned(result);
        });
    }
}

async function showReturned(result) {
    app.rentMessage.innerHTML = `<h4 class="message">${result.movieName} är nu återlämnad.</h4>`;

    getRentedMovies();
}

async function getUpdatedMovies()
{
    fetch('/api/v1/movies', {
        method: 'GET',
        headers: {
            'Content-type': 'application/json; charset=UTF-8',
            'Authorization': `Bearer ${app.globaltoken}`
        }
    })
        .then(resp => resp.json())
        .then(movies => showMovies(movies))
}














