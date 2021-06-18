create table if not exists "user".users
(
    "userId" serial       not null
        constraint users_pk
            primary key,
    username varchar(64)  not null,
    password bytea        not null,
    email    varchar(256) not null
);

create unique index if not exists users_username_uindex
    on "user".users (username);

create unique index if not exists users_email_uindex
    on "user".users (email);

create table if not exists "user".sessions
(
    token    uuid default gen_random_uuid() not null
        constraint sessions_pk
            primary key,
    "userId" integer                        not null
        constraint sessions_users_userid_fk
            references "user".users
);

create unique index if not exists sessions_token_uindex
    on "user".sessions (token);

create unique index if not exists sessions_userid_uindex
    on "user".sessions ("userId");

create table if not exists movie.movies
(
    "Id"            integer not null
        constraint movies_pk
            primary key,
    "Title"         text    not null,
    "OriginalTitle" text    not null,
    "Plot"          text    not null
);

create table if not exists movie.genres
(
    "Id"   integer not null
        constraint genres_pk
            primary key,
    "Name" integer not null
);

create unique index if not exists genres_name_uindex
    on movie.genres ("Name");

create table if not exists movie.movie_genres
(
    movieid integer not null
        constraint movie_genres_movieid_fkey
            references movie.movies,
    genreid integer not null
        constraint movie_genres_genreid_fkey
            references movie.genres,
    constraint movie_genres_pkey
        primary key (movieid, genreid)
);

create or replace function "user".register_user(username character varying, password bytea, email character varying) returns text
    language plpgsql
as
$$
BEGIN
    INSERT INTO "user".users(username, password, email)
    VALUES (username, password, email);
    RETURN 'Operation succeed.';
EXCEPTION
    WHEN check_violation THEN
        RAISE 'Username or password conditions did not get satisfied.';
    WHEN unique_violation THEN
        RAISE 'Username or email is not unique';
    WHEN others THEN
        RAISE 'Something went wrong';
END;
$$;

create or replace function "user".login(_username character varying, _password bytea) returns text
    language plpgsql
as
$$
DECLARE
    _token  uuid = NULL;
    _userId int  = NULL;
BEGIN
    SELECT "userId"
    FROM "user".users
    WHERE username = _username
      AND password = _password
    INTO _userId;
    IF _userId IS NOT NULL THEN
        DELETE FROM "user".sessions WHERE "userId" = _userId;
        INSERT INTO "user".sessions("userId") VALUES (_userId) RETURNING token INTO _token;
        IF _token IS NOT NULL THEN
            RETURN _token;
        END IF;
    END IF;
    RAISE 'Incorrect username or password';
EXCEPTION
    WHEN foreign_key_violation THEN
        RAISE 'Account is removed';
END;
$$;

create or replace function movie.save_movie(_id integer, _name text, _origname text, _plot text) returns text
    language plpgsql
as
$$
BEGIN
    INSERT INTO "movie".movies("Id", "Title", "OriginalTitle", "Plot")
    VALUES (_id, _name, _origName, _plot)
    ON CONFLICT DO NOTHING;
    RETURN 'Succeed';
EXCEPTION
    WHEN others THEN
        RAISE 'Something went wrong';
END;
$$;


