


SET DATEFORMAT dmy;
select [Date of Birth], CONVERT(date, [date of birth], 103) from Participant


select [Date of Birth] from Participant where [Date of Birth] is not null order by [Date of Birth] desc 

SET DATEFORMAT dmy;
select [Date of Birth], ISDATE([Date of Birth]) as dob from Participant where [Date of Birth] is not null order by dob asc

SET DATEFORMAT dmy;

update Participant set [Date of Birth] = null where [Date of Birth] in ('16438','16071','10959','10228','17272','17938')



select ISDATE('23/03/1966')
select ISDATE('04/11/1951')

select * from Participant where nric is null 

select * from Participant where name like '%irene chan%'
select * from Participant where name like '%sim sewo%'

update Participant set NRIC = 'S2560186F' where  name like '%sim sewo%'

delete from Participant where Name = 'IRENE CHAN SIEW YING'

delete from Participant where name = 'TEO AH CHOO'
delete from Participant where name = 'CHUA MENG NGOH' and Address is null 
delete from Participant where name = 'SIM LI NAH' and Occupation is null 

select * from Participant where NRIC like '%nil%'

delete from participant where name = 'JOCELYN BUNGAYAN TAN'
delete from participant where name = 'CHNG CHIW POH'

select * from participant where name like '%JOCELYN BUNGAYAN TAN%'
select * from participant where name like '%CHNG CHIW POH%'

delete from Participant where name = 'POK TECK MUI' and [Year of Visit] = '2014'

select NRIC, [Year of Visit], count([Year of Visit]) as cn from Participant group by nric, [Year of Visit] order by cn desc


select * from Participant where NRIC = 'S0554160C'

delete from Participant where name = 'CHUA JOO ENG'

select * from PreReg

select distinct([Preferred language]) from PreReg

update PreReg set [Preferred language] = 'Mandarin' where [Preferred language] = 'Chinese'