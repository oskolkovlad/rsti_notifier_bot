create table chat
(
	chatid bigint not null,
	username varchar(100) not null,
	firstname varchar(300),
	lastname varchar(300),

	constraint pk_chat primary key (chatid)
);

comment on table chat is 'Список подключенных к боту чатов';
comment on column chat.chatid is 'Идентификатор';
comment on column chat.username is 'Никнейм';
comment on column chat.firstname is 'Имя';
comment on column chat.lastname  is 'Фамилия';

---

create table chatproperty
(
	chatpropertyid varchar(32) not null,
	chatid bigint not null,
	name varchar(100) not null,
	value varchar(100) not null,

	constraint pk_chatproperty primary key (chatpropertyid)
);

comment on table chatproperty is 'Список подключенных к боту чатов';
comment on column chatproperty.chatpropertyid is 'Идентификатор';
comment on column chatproperty.chatid is 'Идентификатор чата';
comment on column chatproperty.name is 'Наименование';
comment on column chatproperty.value is 'Значение';

---

create table news
(
	newsid serial not null,
	title text not null,
	preview text,
	url text not null,
	imageurl text,
	publishdate DATE,

	constraint pk_news primary key (newsid)
);

comment on table news is 'Список опубликованных новостей';
comment on column news.newsid is 'Идентификатор и порядковый номер';
comment on column news.title is 'Заголовок';
comment on column news.preview is 'Краткий пересказ';
comment on column news.url  is 'Ссылка';
comment on column news.imageurl  is 'Ссылка на картинку';
comment on column news.publishdate  is 'Дата публикации';