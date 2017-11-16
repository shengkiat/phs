
use phsFUDataLoading

update DataCollection set [Label Text] = '<p style=''color:red''><b>PLEASE MEASURE PARTICIPANT''S BLOOD PRESSURE. Ensure participant is comfortable at rest before measuring their BP. They should not have taken caffeine or smoked in the past 30 minutes either.

TAKE 1ST BP READING NOW & RECORD ON FORM A.</b></p>' where id = 40 

update DataCollection set [Label Text] = N'If the participant has any concern(s), please take a brief history. (Please write NIL if otherwise).
E.g."Do you have any health issues that you are currently concerned about?" "最近有没有哪里不舒服？”

Please advise that there will be no diagnosis or prescription made at our screening. Kindly advise the participant to see a GP/polyclinic instead if he/she is expecting treatment for their problems.

REFER TO DR CONSULT if worrying problems / participant strongly insists. Indicate on 1) Referrals Tab 2) Page 2 of Form A.' where id = 42

update DataCollection set [Label Text] = 'Please rule out red flags based on participants''s health concerns. Below is a non-exhaustive list of possible red flags:
- Constitutional Symptoms: LOA, LOW
- CVS: Chest pain
- Respi: SOB
- GI: change in BO habits, PR bleed
- GU: urination problem
- Frequent falls 

REFER TO DR CONSULT if worrying / patient strongly insists. Indicate on 1) Referrals Tab 2) Page 2 of Form A.' where id = 46 

update DataCollection set [Label Text] = '13. Does the participant drink alcohol regularly?

Please use the following guideline to judge as to whether participant is drinking excessive alcohol. ''Men should drink no more than 2 standard drinks a day, and women, no more than 1. 

A standard alcoholic drink is defined as can (330 ml) regular beer, Half glass (175 ml) wine or 1 nip (35 ml) spirit.''' where id = 79 

update DataCollection set [Label Text] = 'REFER TO SOCIAL SUPPORT STATION if participant is in need & willing to seek financial aid. Please indicate on 1) Referrals Tab 2) Page 2 of Form A.

Note the following criteria for your assessment: 
Per-capita monthly income for CHAS: Orange Card: $1101- $1800; Blue Card: $1100 and below' where id = 100

update DataCollection set [Label Text] = 'The 8 items of this schedule require raters to make a judgement as to whether the proposition under “Assessment” is satisfied or not. Each question must be asked exactly as shown but follow-up or subsidiary questions may be used to clarify the initial answer.
Select 1 = Fits the assessment criteria; Select 0 = Does not fit the criteria; participant is well.' where id = 182

update DataCollection set [Label Text] = '3.     What do you do for exercise?
*Take down salient points. 
*Dangerous/ inappropriate exercises refer to participants feeling pain or having difficulty performing the exercise.
*If exercises are dangerous or deemed inappropriate, to REFER FOR PHYSIOTHERAPIST CONSULATION. ' where id = 231

update DataCollection set [Label Text] = '4. Illnesses: For 11 illnesses, participants are asked, “Did a doctor ever tell you that you have [illness]?” 
The illnesses include hypertension, diabetes, cancer (other than a minor skin cancer), chronic lung disease, heart attack, congestive heart failure, angina, asthma, arthritis, stroke, and kidney disease.

The total illnesses (0–11) are recorded as 
0–4 = 0 and 5–11 = 1.
' where id = 241

update DataCollection set [Label Text] = '5. Loss of weight: How much do you weigh with your clothes on but without shoes? [current weight] 
One year ago, in October 2016, how much did you weigh without your shoes and with your clothes on? [weight 1 year ago]. 

Percent weight change is computed as: 
[[weight 1 year ago - current weight]/weight 1 year ago]] * 100.
What is the percentage (%) weight change?

Percent change > 5 (representing a 5% loss of weight) is scored as 1 and < 5 as 0.

If participant cannot remember his/her weight, ask if there was any significant loss in weight the past year.' where id = 242

update DataCollection set [Label Text] = '1) Participants will be eligible for the SWCDC Safe and Bright Homes Programme if they meet the following criteria:
i) SWCDC Resident (Link: <a>http://sis.pa-apps.sg/NASApp/sim/SearchResults.jsp</a>)
ii) Requires home modification (determined by SAOT) - Refer to Form A
' where id = 309

update DataCollection set [Label Text] = '2) Do you wish to sign up for the SWCDC Safe and Bright Homes Programme?

Persuade participant to sign up for SWCDC Safe and Bright Homes. 
Description of the programme: “The Safe and Bright Homes programme aims to develop safer and more energy-efficient homes for senior citizens and persons with disabilities. Safety (e.g. bathroom grab bars, non-slip mats etc), energy and water conservation features (energy-saving bulbs, water thimbles and cistern bags etc) will be installed in selected homes of needy residents. Workshops will also be conducted to teach them how to troubleshoot common household problems. The programme will be spread out over 10 sessions, for about 10 months.”' where id = 310

update DataCollection set [Label Text] = N'11. Please read and acknowledge the following eligibility criteria for Phlebotomy 抽血合格标准:
- Fasted for at least 10 hours 十个小时内没有吃东西或喝饮料
- Have not done a blood test in past 1 year 一年内没有做抽血检查
- Have not been previously diagnosed with diabetes mellitus OR hyperlipidemia OR hypertension 没有被诊断患有高血压 或 高血脂 或 高血压' where id = 329

update DataCollection set [Label Text] = 'I hereby give my consent to the Public Health Service Executive Committee to collect my personal information for the purpose of participating in the Public Health Service (hereby called “PHS”) and its related events, and to contact me via calls, SMS, text messages or emails regarding the event and follow-up process.

Should you wish to withdraw your consent for us to contact you for the purposes stated above, please notify a member of the PHS Executive Committee at ask.phs@gmail.com in writing. We will then remove your personal information from our database. Please allow 3 business days for your withdrawal of consent to take effect. All personal information will be kept confidential, will only be disseminated to members of the PHS Executive Committee, and will be strictly used by these parties for the purposes stated.' where id = 331

update DataCollection set [Label Text] = '1. Introduction

This is a community healthcare screening programme (the “Programme”) jointly organised by the National University Health System (“NUHS”), National University Health Services Group Pte Ltd (“NUHSG”) ((formerly known as “Jurong Health Services Pte. Ltd.”) which includes Ng Teng Fong General Hospital, Jurong Community Hospital & Jurong Medical Centre), the People’s Association (“PA”), our appointed healthcare institutions , clinics, service providers, healthcare professionals and/or relevant third parties (including National University of Singapore’s medical students and Southwest CDC).

(collectively referred to as  the “Organisers”).
' where id = 371

update DataCollection set [Label Text] = '2. Consent to Screen

I consent to undergo health screening tests for one or more of the following:  (i) chronic diseases (obesity, diabetes, high blood pressure and high blood cholesterol, and / or (ii) cancers (breast and cervical for females, and colorectal cancer for males and females)( collectively,  the “Test”), and / or (iii) geriatrics assessment, and / or (iv) oral health assessment ( collectively, the "Test") and follow-ups related to the Test. I understand that all my personal data and information will be recorded and stored in a secured and confidential manner by the Organisers. ' where id = 372

update DataCollection set [Label Text] = '4. Commitment to Follow-Up

I understand that I may be contacted regardless of my Test result for administrative reasons.

In the event of any abnormal Test result, I understand that I will be contacted for follow-up using my contact details provided herein and that I should see a doctor if any of my Test result is abnormal. I also understand that there are limitations to the Test and that the Test is for screening purposes and that further tests, including confirmatory tests, may be necessary in detecting or ruling out medical risk factors or conditions. I should seek medical advice if I feel unwell or have any symptom even if the Test result is normal.

Depending on my Test result, I may be contacted and/or referred by one of the Organisers or the appointed service providers for post-screening follow-up within the Programme. I may also be referred, where appropriate, to participate in community programmes/activities organised by the PA, Health Promotion Board (“HPB”), constituency managers, service providers or grassroots organisations for follow-up or participation in community programmes/activities. I understand that the decision to participate in the above-mentioned activities is entirely mine.
' where id = 374

update DataCollection set [Label Text] = '5. Collection and Use of Information  
I acknowledge that my personal data and relevant screening and follow-up information, including the Test results, data from Breast Screening Singapore, Cervical Screening Singapore or Singapore Cancer Society (collectively, the “Information”) will be collected and used by the Organisers for the purposes of conducting the Test and managing and implementing follow-up action arising from the Test results. I also acknowledge that the Information will be retained by NUHS, NUHSG, PA, HPB, the National e-Health Records (NEHR), Integrated Health Information Systems Pte. Ltd (IHiS), MOH Holdings Pte. Ltd (MOHH) and the Ministry of Health (MOH). I also acknowledge that aggregate and/or my de-identified Information may be used for research, statistical and planning purposes.' where id = 375

update DataCollection set [Label Text] = '6. Authorisation
I authorise the Organisers and/or Service Providers to approach other healthcare institutions, clinics, and/or relevant third parties who are in possession of my medical records, which includes but not limited to screening, follow-up, further assessment and/or treatment records relevant to the Programme and to request for such records for the purposes of further tests, patient care, treatment or clinical/programme review. 
' where id = 376

update DataCollection set [Label Text] = '7. Disclosure of Information
Unless otherwise indicated below, I consent to the Organisers and HPB directly disclosing my Information to HPB’s Collaborators  for the purposes of checking whether I would require re-screening, further tests, follow-up action and/or referral to community programmes / activities.' where id = 377


-- telehealth
update DataCollection set [Label Text] =  N'Compliance to the Personal Data Protection Act (PDPA): Consent has previously been given to the PHS Committee to collect participants'' personal information for both the screening and the follow-up process. Terms and conditions as stated in the PHS screening registration and consent form will stand.' where id = 400

update DataCollection set [Label Text] =  N'Compliance to the Personal Data Protection Act (PDPA): Consent has previously been given to the PHS Committee to collect participants'' personal information for both the screening and the follow-up process. Terms and conditions as stated in the PHS screening registration and consent form will stand.' where id = 421

update DataCollection set [Label Text] =  N'16. Apologize, and explain that unfortunately, we will not be able to change to the GP clinic that he had already selected. As his health report is already delivered to the clinic, encourage him to visit the clinic to collect his results. Then, thank the participant and end the call.' where id = 438


update DataCollection set [Label Text] =  N'Compliance to the Personal Data Protection Act (PDPA): Consent has previously been given to the PHS Committee to collect participants'' personal information for both the screening and the follow-up process. Terms and conditions as stated in the PHS screening registration and consent form will stand.' where id = 443

update DataCollection set [Label Text] =  N'Compliance to the Personal Data Protection Act (PDPA): Consent has previously been given to the PHS Committee to collect participants'' personal information for both the screening and the follow-up process. Terms and conditions as stated in the PHS screening registration and consent form will stand.' where id = 455


update DataCollection set [Label Text] =  N'8. As this will be our last follow-up call to this participant, remind him that he should claim his free doctor''s consultation within the next 2-3 weeks (before 5 Feb 2018). Otherwise, the free doctor''s consultation will expire. His results will then be mailed directly to his address. ' where id = 464


update DataCollection set [Label Text] =  N'10. If you have anything about the call that you would like us to know, please record it below. If not, please save this form, and update the call status to PHASE II DONE. If there was a change in address, update the call status to ADDRESS CHANGED, PHASE II DONE. You may now move on to your next participant. Thank you!' where id = 466




select id, [Label Text], LEN([Label Text]), textlength from DataCollection where LEN([Label Text]) < textlength order by id 

select id, [Label Text] from datacollection where [Label Text] like '%??%'

