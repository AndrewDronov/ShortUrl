CREATE TABLE public.url (
    token character varying(8) DEFAULT "substring"(md5((random())::text), 0, 9) NOT NULL,
    link text NOT NULL
);


ALTER TABLE ONLY public.url
    ADD CONSTRAINT url_pk PRIMARY KEY (token);