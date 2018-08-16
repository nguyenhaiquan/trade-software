alter table portfolio
drop constraint fk_portpolio_investor

alter table investor
drop constraint pk_investor;

alter table investor
alter column code nvarchar(50) not null;

alter table portfolio
alter column investorCode nvarchar(50) not null;

alter table investor
add constraint pk_investor
primary key (code);

alter table portfolio
add constraint fk_portfolio_investor
foreign key (investorCode)
references investor(code);
