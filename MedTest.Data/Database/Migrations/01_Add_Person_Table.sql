create table if not exists public.person(
	id serial not null primary key,
	first_name text not null,
	last_name text not null
);