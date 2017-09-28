
use phsDataLoading

update DataCollection set [Label Text] = '<p style=''color:red''><b>PLEASE MEASURE PARTICIPANT''S BLOOD PRESSURE. Ensure participant is comfortable at rest before measuring their BP. They should not have taken caffeine or smoked in the past 30 minutes either.

TAKE 1ST BP READING NOW & RECORD ON FORM A.</b></p>' where id = 32 

update DataCollection set [Label Text] = N'If the participant has any concern(s), please take a brief history. (Please write NIL if otherwise).
E.g."Do you have any health issues that you are currently concerned about?" "最近有没有哪里不舒服？”

Please advise that there will be no diagnosis or prescription made at our screening. Kindly advise the participant to see a GP/polyclinic instead if he/she is expecting treatment for their problems.

REFER TO DR CONSULT if worrying / participant strongly insists. Indicate on 1) Referrals Tab 2) Page 2 of Form A.' where id = 34

update DataCollection set [Label Text] = 'Please rule out red flags based on participants''s health concerns. Below is a non-exhaustive list of possible red flags:
- Constitutional Symptoms: LOA, LOW
- CVS: Chest pain
- Respi: SOB
- GI: change in BO habits, PR bleed
- GU: urination problem

REFER TO DR CONSULT if worrying / patient strongly insists. Indicate on 1) Referrals Tab 2) Page 2 of Form A.' where id = 38 

update DataCollection set [Label Text] = '13. Does the participant drink alcohol regularly?

Please use the following guideline to judge as to whether participant is drinking excessive alcohol. ''Men should drink no more than 2 standard drinks a day, and women, no more than 1. 

A standard alcoholic drink is defined as can (330 ml) regular beer, Half glass (175 ml) wine or 1 nip (35 ml) spirit.''' where id = 70 

update DataCollection set [Label Text] = 'REFER TO SOCIAL SUPPORT STATION if participant is in need & willing to seek financial aid. Please indicate on 1) Referrals Tab 2) Page 2 of Form A.

Note the following criteria for your assessment: 
Per-capita monthly income for CHAS: Orange Card: $1101- $1800; Blue Card: $1100 and below' where id = 91

update DataCollection set [Label Text] = 'The 8 items of this schedule require raters to make a judgement as to whether the proposition under “Assessment” is satisfied or not. Each question must be asked exactly as shown but follow-up or subsidiary questions may be used to clarify the initial answer.
Select 1 = Fits the assessment criteria; Select 0 = Does not fit the criteria; participant is well.' where id = 157 

update DataCollection set [Label Text] = '3.     What do you do for exercise?
*Take down salient points. 
*Dangerous/ inappropriate exercises refer to participants feeling pain or having difficulty performing the exercise.
*If exercises are dangerous or deemed inappropriate, to REFER FOR PHYSIOTHERAPIST CONSULATION. ' where id = 195

update DataCollection set [Label Text] = '4. Illnesses: For 11 illnesses, participants are asked, “Did a doctor ever tell you that you have [illness]?” 
The illnesses include hypertension, diabetes, cancer (other than a minor skin cancer), chronic lung disease, heart attack, congestive heart failure, angina, asthma, arthritis, stroke, and kidney disease.

The total illnesses (0–11) are recorded as 
0–4 = 0 and 5–11 = 1.' where id = 205

update DataCollection set [Label Text] = '5. Loss of weight: How much do you weigh with your clothes on but without shoes? [current weight] 
One year ago, in October 2016, how much did you weigh without your shoes and with your clothes on? [weight 1 year ago]. 

Percent weight change is computed as: 
[[weight 1 year ago - current weight]/weight 1 year ago]] * 100.
What is the percentage (%) weight change?

Percent change > 5 (representing a 5% loss of weight) is scored as 1 and < 5 as 0.

If participant cannot remember his/her weight, ask if there was any significant loss in weight the past year.
' where id = 206

update DataCollection set [Label Text] = '1) Participants will be eligible for the SWCDC Safe and Bright Homes Programme if they meet the following criteria:
i) SWCDC Resident (Link: <a>http://sis.pa-apps.sg/NASApp/sim/SearchResults.jsp</a>)
ii) Requires home modification (determined by SAOT) - Refer to Form A
' where id = 270

update DataCollection set [Label Text] = '2) Do you wish to sign up for the SWCDC Safe and Bright Homes Programme?

Persuade participant to sign up for SWCDC Safe and Bright Homes. 
Description of the programme: “The Safe and Bright Homes programme aims to develop safer and more energy-efficient homes for senior citizens and persons with disabilities. Safety (e.g. bathroom grab bars, non-slip mats etc), energy and water conservation features (energy-saving bulbs, water thimbles and cistern bags etc) will be installed in selected homes of needy residents. Workshops will also be conducted to teach them how to troubleshoot common household problems. The programme will be spread out over 10 sessions, for about 10 months.”' where id = 271

update DataCollection set [Label Text] = N'11. Please read and acknowledge the following eligibility criteria for Phlebotomy 抽血合格标准:
- Fasted for at least 10 hours 十个小时内没有吃东西或喝饮料
- Have not done a blood test in past 1 year 一年内没有做抽血检查
- Have not been previously diagnosed with diabetes mellitus OR hyperlipidemia OR hypertension 没有被诊断患有高血压 或 高血脂 或 高血压' where id = 292

update DataCollection set [Label Text] = 'I hereby give my consent to the Public Health Service Executive Committee to collect my personal information for the purpose of participating in the Public Health Service (hereby called “PHS”) and its related events, and to contact me via calls, SMS, text messages or emails regarding the event and follow-up process.

Should you wish to withdraw your consent for us to contact you for the purposes stated above, please notify a member of the PHS Executive Committee at ask.phs@gmail.com in writing. We will then remove your personal information from our database. Please allow 3 business days for your withdrawal of consent to take effect. All personal information will be kept confidential, will only be disseminated to members of the PHS Executive Committee, and will be strictly used by these parties for the purposes stated.' where id = 294



select id, [Label Text], LEN([Label Text]), textlength from DataCollection where LEN([Label Text]) < textlength order by id 

select id, [Label Text] from datacollection where [Label Text] like '%??%'

