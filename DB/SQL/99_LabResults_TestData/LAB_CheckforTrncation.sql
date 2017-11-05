
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
update DataCollection set [Label Text] =  N'Compliance to the Personal Data Protection Act (PDPA): Consent has previously been given to the PHS Committee to collect participants'' personal information for both the screening and the follow-up process. Terms and conditions as stated in the PHS screening registration and consent form will stand. ' where id = 402
update DataCollection set [Label Text] =  N'"Why you''re calling:

To put things in context, 
PHS participants would have received an SMS or phone call about 2 weeks ago, from Jurong Health Service. Through the SMS or call, they would have been notified that their overall health screening result is ""not ideal”, and that they are required to collect their health report at their selected GP clinic. They would also have been reminded of the GP clinic at that they chose at the Registration Counter of the Screening.

Ideally, participants would have visited their selected GP clinic to collect their health report and blood test results, and also to go for their free doctor''s consultation.

Thus, the objectives of this call are to:
1. First, check if the participant has received an SMS or phone call from Jurong Health Service.
2. Check if the participant has gone for his free doctor''s consultation at his selected GP clinic." ' where id = 405
update DataCollection set [Label Text] =  N'Scroll up to see your participant''s overall screening results. The non-ideal values of the health report &/ blood test should be highlighted for you. The participant''s selected GP clinic (and details for walk-in/appointment) is also stated for your reference later on!  ' where id = 409
update DataCollection set [Label Text] =  N'"1. Formalities - self intro, remind participant that s/he attended PHS Screening Event on 21/22 Oct at Jurong East, inform that the purpose of call is to follow-up on the screening.

2. Verify participant identity by asking for NRIC (last 4 digits)

3. Check if participant had received an SMS/phone call from Jurong Health Service.

4. First, check if he has already gone for his free doctor''s consultation at his selected GP clinic. 

- If he has, ask him about his experience at the consultation.
- If he hasn''t, strongly encourage him to visit his selected GP clinic ASAP, as the free consultation will expire in about 3 months'' time.
Pro Tip: It''s good to find out why he hasn''t seen a doctor, and then tailor your advice to these reasons. You may also talk about the benefits of early treatment or the detriments of not receiving treatment too (refer to Telehealth Materials for Volunteers for more information!)

5. Thank the participant and end the call. " ' where id = 412
update DataCollection set [Label Text] =  N'"Oh no ):

Do make sure that you try calling this participant again - at least 3 times on 3 separate occasions - before the end of Telehealth Phase I (ends 29 Nov 2017). 

Remember that the onus is now on you to make sure that the call goes through; otherwise, this participant may never go to see the doctor to review his abnormal blood test results!

In the meantime, please press ''save'' on this form, and update the ''CALL STATUS'' of this participant, to ""Did not pick up on 1st call"". 

You may now move on to call the next participant on your list. Thank you!" ' where id = 417
update DataCollection set [Label Text] =  N'"Oh no ):

Do make sure that you try calling this participant again - at least 3 times on 3 separate occasions - before the end of Telehealth Phase I (ends 29 Nov 2017). 

Remember that the onus is now on you to make sure that the call goes through; otherwise, this participant may never go to see the doctor to review his abnormal blood test results!

In the meantime, please press ''save'' on this form, and update the ''CALL STATUS'' of this participant, to ""Did not pick up on 2nd call"". 

You may now move on to call the next participant on your list. Thank you!" ' where id = 418
update DataCollection set [Label Text] =  N'"Thank you for your hard work! It is unfortunate that the participant did not pick up the call, and this could be due to a variety of reasons. Nevertheless, we really appreciate your efforts in linking up with the participant. 

Please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Did not pick up on 3rd call''.

Thank you! You may now move on to call another participant." ' where id = 419
update DataCollection set [Label Text] =  N'"Oh no ):

Please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Invalid/wrong number'', so that we know of this problem and can take over contacting the participant from here. 

Thank you! You may now move on to call another participant." ' where id = 420
update DataCollection set [Label Text] =  N'"1. Formalities - intro yourself, remind him of PHS (i.e. the ""free health screening that he attended in Jurong East on 21/22 October""), and say that this call''s purpose is to follow-up on the screening

2. Verify participant''s identity by asking for NRIC number (last 4 digits)

3. Check if he had received an SMS/phone call from Jurong Health Service, informing them to visit his selected GP clinic to collect his results." ' where id = 421
update DataCollection set [Label Text] =  N'"4. Apologise, and proceed to update him that:
- He will need to visit his selected GP clinic to collect his health report and blood test results.
- His selected GP clinic is the one stated above. Please inform him of the walk-in/appointment details, and what he has to bring down (refer to Telehealth PDF for more details).
- At this clinic, he will be eligible for a free first doctor''s consultation, where the doctor will explain and discuss his results with him.

5. Remind the participant that the free consultation would only be valid for about 3 months'' time, hence he should visit the selected GP clinic asap.

6. Thank the participant and end the call! Press ""Done!"" to continue." ' where id = 422
update DataCollection set [Label Text] =  N'"8. Referring to the GP clinic details on the Telehealth PDF, remind him of the walk-in/appointment details of the clinic, the address, and what he has to bring when he visits the clinic. 

9. Assure the participant that his results will be delivered to the correct GP clinic that he has just selected.

10. Encourage him to see the doctor for his abnormal screening results asap!
- This is VERY important!!
- Further encourage him by explaining the benefits of early treatment and the potential adverse consequences of not doing so. 

11. Thank the participant and end the call. Press ''Done!'' to continue." ' where id = 428
update DataCollection set [Label Text] =  N'"6. Encourage him to see the doctor for his abnormal screening results asap!
- This is VERY important!!
- Further encourage him by explaining the benefits of early treatment and the potential adverse consequences of not doing so.
- Also, by providing a free doctor''s consultation directly at the selected GP clinic, it is made more convenient for him to visit the clinic and have his results explained to him at the same time. 

7. Thank the participant and end the call. Press ''Done!'' to continue." ' where id = 429
update DataCollection set [Label Text] =  N'"6. Apologise and inform the participant that, unfortunately, we will not be allowing changes to the original GP clinic selected. 
As his health report is already delivered to the GP clinic, please encourage him to visit that GP clinic to collect his results.

9. Highlight to him that he will be eligible for a free doctor''s consultation at that particular GP clinic if he makes the visit down to collect his results. 

10. Encourage him to see the doctor for his abnormal screening results asap!
- This is VERY important!!
- Further encourage him by explaining the benefits of early treatment and the potential adverse consequences of not doing so.
- Also, by providing a free doctor''s consultation directly at the selected GP clinic, it is made more convenient for him to visit the clinic and have his results explained to him at the same time. 

11. Thank the participant and end the call. Press ''Done!'' to continue." ' where id = 430
update DataCollection set [Label Text] =  N'"HOORAY, you''re done! 
If any, please specify any additional important remarks that you''d like us to know about your call. If you have none, please indicate NIL. 
Please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Telehealth completed''. 

THANK YOU SO MUCH!" ' where id = 431
update DataCollection set [Label Text] =  N'"HOORAY, you''re done! 
If any, please specify any additional important remarks that you''d like us to know about your call. If you have none, please indicate NIL. 
Please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Telehealth completed, ready for Phase II. *To resend documents to correct GP clinic''. 

THANK YOU SO MUCH!" ' where id = 432
update DataCollection set [Label Text] =  N'"HOORAY, you''re done! 
If any, please specify any additional important remarks that you''d like us to know about your call. If you have none, please indicate NIL. 
Please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Telehealth completed, ready for Phase II''. 

THANK YOU SO MUCH!" ' where id = 433
update DataCollection set [Label Text] =  N'Compliance to the Personal Data Protection Act (PDPA): Consent has previously been given to the PHS Committee to collect participants'' personal information for both the screening and the follow-up process. Terms and conditions as stated in the PHS screening registration and consent form will stand. ' where id = 437
update DataCollection set [Label Text] =  N'"Why you''re calling:

To put things in context, 
PHS participants would have received their screening results in their mailboxes about 2 weeks ago, prior to this call. Through the health report, they would have been notified that their overall health screening result is ""not ideal”. It would have been recommended for them to visit their family doctor soon with these results. 

Ideally, participants would have consulted a doctor about their screening results by now.

Thus, the objectives of this call are to:
1. First, check if the participant has received his screening results in the mail.
2. Check if the participant has visited a doctor to discuss the screening results." ' where id = 440
update DataCollection set [Label Text] =  N'"1. Formalities - introduce yourself, remind participant that s/he attended PHS Screening Event on 21/22 Oct at Jurong East. Inform that the purpose of call is to follow-up on the screening.

2. Verify participant identity by asking for NRIC (last 4 digits)

3. Check if participant had received PHS envelope in mailbox, and that it contained:
a) Health Screening Report,
b) Blood Test Report (if applicable, see if participant''s screening results include blood test values).

4. First, check if he has already consulted a doctor about his screening results.
- If he has, ask him about his experience at the consultation.
- If he hasn''t, strongly encourage him to see a doctor ASAP for his detected screening abnormalities.

Pro Tip: It''s good to find out why he hasn''t seen a doctor, and then tailor your advice to these reasons. You may also talk about the benefits of early treatment or the detriments of not receiving treatment too.

5. Thank the participant and end the call. " ' where id = 447
update DataCollection set [Label Text] =  N'"Oh no ):

Do make sure that you try calling this participant again - at least 3 times on 3 separate occasions - before the end of Telehealth Phase I (ends 29 Nov 2017). 

Remember that the onus is now on you to make sure that the call goes through; otherwise, this participant may never go to see the doctor to review his abnormal blood test results!

In the meantime, please press ''save'' on this form, and update the ''CALL STATUS'' of this participant, to ""Did not pick up on 1st call"". 

You may now move on to call the next participant on your list. Thank you!" ' where id = 452
update DataCollection set [Label Text] =  N'"Oh no ):

Do make sure that you try calling this participant again - at least 3 times on 3 separate occasions - before the end of Telehealth Phase I (ends 29 Nov 2017). 

Remember that the onus is now on you to make sure that the call goes through; otherwise, this participant may never go to see the doctor to review his abnormal blood test results!

In the meantime, please press ''save'' on this form, and update the ''CALL STATUS'' of this participant, to ""Did not pick up on 2nd call"". 

You may now move on to call the next participant on your list. Thank you!" ' where id = 453
update DataCollection set [Label Text] =  N'"Thank you for your hard work! It is unfortunate that the participant did not pick up the call, and this could be due to a variety of reasons. Nevertheless, we really appreciate your efforts in linking up with the participant. 

Please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Did not pick up on 3rd call''.

Thank you! You may now move on to call another participant." ' where id = 454
update DataCollection set [Label Text] =  N'"Oh no ):

Please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Invalid/wrong number'', so that we know of this problem and can take over contacting the participant from here. 

Thank you! You may now move on to call another participant." ' where id = 455
update DataCollection set [Label Text] =  N'"1. Formalities - intro yourself, remind him of PHS (i.e. the ""free health screening that he attended in Jurong East on 21/22 October""), and say that this call''s purpose is to follow-up on the screening

2. Verify participant''s identity by asking for NRIC number (last 4 digits)

3. Check if he''s received his screening results in an envelope from PHS in his mailbox (was mailed out 2 weeks ago)

4. Check that it contains: health report, and his blood test results (if applicable)." ' where id = 456
update DataCollection set [Label Text] =  N'"5. Ask the participant which of the items is missing (Health Report, or Blood Test Results) and record it in the textbox below.

6. Apologise, and tell him that PHS will mail him a new copy with the correct documents ASAP.

7. Inform him that after he receives the correct documents, he should follow the recommendations stated in his health report - which would be that he should visit his own family doctor with his results, so that the doctor can advise him on what he should do next.

8. Thank him and end the call.

So sorry for the error on our part! We will definitely troubleshoot from here and mail this participant the correct envelope contents :) 

Now, please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Received envelope, not all items present'' so that we can identify this participant. 

You may move on to call your next participant. Thank you!" ' where id = 457
update DataCollection set [Label Text] =  N'"5. Tell him to check his mailbox asap for the envelope, and that you will call back in the next few days to check the contents and run through it with him. 

6. Thank him and end the call.

For now, please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Hasn''t checked mailbox for envelope''.
Please remember to call this participant again in the next few days, as you still need to explain the contents of his envelope to him. 

In the meantime, you may move on to call your next participant. Thank you!" ' where id = 458
update DataCollection set [Label Text] =  N'"5. Apologise and explain that this could be because his address was recorded wrongly during the screening.

6. Politely ask for his address again, and record it below in the textbox. Confirm with him again that what you recorded is correct.

7. Inform him that a new envelope will be sent to him soon.

8. Thank him for his time and end the call. 

We will definitely troubleshoot from here and mail the envelope to the correct address that you''ve just keyed in for us.

Now, please press ""save"" on this form, and update the ''CALL STATUS'' of this participant to ''Checked mailbox but didn''t receive envelope''. 

Once done, you can move on to call your next participant. Thank you!" ' where id = 459
update DataCollection set [Label Text] =  N'"6. Refer him to his Screening Results and Blood Test results. You may view them at the top of this form, if necessary.
Highlight his blood pressure readings and/or some blood test values were found to lie outside the ideal range.

7. Encourage him to see the doctor for his abnormal screening results asap!
- This is VERY important!!
- Further encourage him by explaining the benefits of early treatment and the potential adverse consequences of not doing so.

8. Remind the participant that he will be called in about another 6 weeks, to continue to follow-up on him.

9. Thank the participant and end the call. Press ''Done!'' to continue." ' where id = 462
update DataCollection set [Label Text] =  N'"HOORAY, you''re done! 
If any, please specify any additional important remarks that you''d like us to know about your call. If you have none, please indicate NIL. 
Please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Telehealth completed''. " ' where id = 463
update DataCollection set [Label Text] =  N'"HOORAY, you''re done! 
If any, please specify any additional important remarks that you''d like us to know about your call. If you have none, please indicate NIL. 
Please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Telehealth Phase I completed, ready for Phase II''. " ' where id = 464

update DataCollection set [Label Text] =  N'Compliance to the Personal Data Protection Act (PDPA): Consent has previously been given to the PHS Committee to collect participants'' personal information for both the screening and the follow-up process. Terms and conditions as stated in the PHS screening registration and consent form will stand.' where id = 468




update DataCollection set [Label Text] =  N'"Who you''re calling:
PHS participants found to have abnormal screening results from our screening, and are eligible for a free doctor''s consultation. We have previously followed up on them through Phase I of Telehealth, and it is now Phase II of calling them!" ' where id = 470
update DataCollection set [Label Text] =  N'"Why you''re calling:

To put things in context, 
PHS participants would have received an SMS or phone call about 2 weeks after the screening. Through the SMS or call, they would have been notified that their overall health screening result is ""not ideal”, and that they are required to collect their health report at their selected GP clinic. They would also have been reminded of the GP clinic at that they chose at the Registration Counter of the Screening.

In Phase I, after contacting this participant, we found that he/she has not gone for their free doctor''s consultation. We would have reminded him to visit his selected GP clinic asap to collect and discuss his results with the doctor.
Ideally, participants would have visited their selected GP clinic to collect their health report and blood test results, and also to go for their free doctor''s consultation.

Thus, the objectives of this Phase II call is to:
1. Check if the participant has visited the selected GP clinic to collect his results, and go for his free doctor''s consultation." ' where id = 471
update DataCollection set [Label Text] =  N'Scroll up to see your participant''s overall screening results. The non-ideal values of the health report &/ blood test should be highlighted for you. The participant''s selected GP clinic (and details for walk-in/appointment) is also stated for your reference later on!  ' where id = 475
update DataCollection set [Label Text] =  N'"1. Formalities - self intro, remind participant that s/he attended PHS Screening Event on 21/22 Oct at Jurong East, inform that the purpose of call is to follow-up on the screening.

2. Verify participant identity by asking for NRIC (last 4 digits)

3. Remind participant that we had called him in November last year (Telehealth Phase I call), also for the purpose of follow-up. He should remember!

4. First, check if he has already gone for his free doctor''s consultation at his selected GP clinic. 
- If he has, ask him about his experience at the consultation.
- If he hasn''t, strongly encourage him to visit his selected GP clinic ASAP, as the free consultation will expire in about 2-3 weeks'' time. 

5. Thank the participant and end the call. " ' where id = 478
update DataCollection set [Label Text] =  N'"Oh no ):

Do make sure that you try calling this participant again - at least 3 times on 3 separate occasions - before the end of Telehealth Phase II (ends 21 Jan 2018). 

Remember that the onus is now on you to make sure that the call goes through; otherwise, this participant may never go to see the doctor to review his abnormal blood test results!

In the meantime, please press ''save'' on this form, and update the ''CALL STATUS'' of this participant, to ""Did not pick up on 1st call (Phase II)"". 

You may now move on to call the next participant on your list. Thank you!" ' where id = 483
update DataCollection set [Label Text] =  N'"Oh no ):

Do make sure that you try calling this participant again - at least 3 times on 3 separate occasions - before the end of Telehealth Phase II (ends 21 Jan 2018). 

Remember that the onus is now on you to make sure that the call goes through; otherwise, this participant may never go to see the doctor to review his abnormal blood test results!

In the meantime, please press ''save'' on this form, and update the ''CALL STATUS'' of this participant, to ""Did not pick up on 2nd call (Phase II)"". 

You may now move on to call the next participant on your list. Thank you!" ' where id = 484
update DataCollection set [Label Text] =  N'"Thank you for your hard work! It is unfortunate that the participant did not pick up the call, and this could be due to a variety of reasons. Nevertheless, we really appreciate your efforts in linking up with the participant. 

Please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Did not pick up on 3rd call (Phase II)''.

Thank you! You may now move on to call another participant." ' where id = 485
update DataCollection set [Label Text] =  N'"1. Formalities - self intro, remind participant that s/he attended PHS Screening Event on 21/22 Oct at Jurong East, inform that the purpose of call is to follow-up on the screening.

2. Verify participant identity by asking for NRIC (last 4 digits)

3. Remind participant that we had called him in November last year (Telehealth Phase I call), also for the purpose of follow-up. He should remember!

4. Check if he has already gone for his free doctor''s consultation at his selected GP clinic. " ' where id = 486
update DataCollection set [Label Text] =  N'"5. Encourage him to see the doctor for his abnormal screening results asap!
- This is VERY important!!
- You may ask him why he still has not gone to see the doctor to collect his results, and try to encourage him using that point of view.
- Further encourage him by explaining the benefits of early treatment and the potential adverse consequences of not doing so.

6. As this will be our last call to him, remind him that:
- He should visit the selected GP clinic within the next 2-3 weeks (before 5 Feb 2018).
- Otherwise, after 5 Feb 2018, his free doctor''s consultation will ""expire"". His results will then be mailed directly to his address.

7. At this point, check if his address (stated at the top of this form) is correct. If not, ask and enter the correct address into the textbox below." ' where id = 489
update DataCollection set [Label Text] =  N'"HOORAY, you''re done! 
If any, please specify any additional important remarks that you''d like us to know about your call. If you have none, please indicate NIL. 
Please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to:
- ''Telehealth Phase II completed'', if there were no changes to his home address.
- ''Telehealth Phase II completed, home address changed'', if there were changes to his home address.

THANK YOU SO MUCH!" ' where id = 491
update DataCollection set [Label Text] =  N'"HOORAY, you''re done! 
If any, please specify any additional important remarks that you''d like us to know about your call. If you have none, please indicate NIL. 
Please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Telehealth Phase II completed''. 

THANK YOU SO MUCH!" ' where id = 492
update DataCollection set [Label Text] =  N'Compliance to the Personal Data Protection Act (PDPA): Consent has previously been given to the PHS Committee to collect participants'' personal information for both the screening and the follow-up process. Terms and conditions as stated in the PHS screening registration and consent form will stand. ' where id = 496
update DataCollection set [Label Text] =  N'"Why you''re calling:

To put things in context, 
PHS participants would have received their screening results in their mailboxes about 2 weeks after the screening. Through the health report, they would have been notified that their overall health screening result is ""not ideal”. It would have been recommended for them to visit their family doctor soon with these results. 

In Phase I, after contacting this participant, we found that he/she has not consulted a doctor about their screening results. We have reminded him to visit his own doctor asap.

Thus, the objectives of this Phase II call is to:
1. Check if the participant has visited a doctor to discuss the screening results." ' where id = 499
update DataCollection set [Label Text] =  N'"1. Formalities - introduce yourself, remind participant that s/he attended PHS Screening Event on 21/22 Oct 2017 at Jurong East. Inform that the purpose of call is to follow-up on the screening.

2. Verify participant identity by asking for NRIC (last 4 digits)

3. Remind him that we had called him in November last year (Telehealth Phase I call), also for the purpose of follow-up. He should remember!

4. Check if he has already consulted a doctor about his screening results.
- If he has, ask him about his experience at the consultation.
- If he hasn''t, strongly encourage him to see a doctor ASAP for his detected screening abnormalities.
*Remind him that his blood test results will only be valid (as in accurate) for the next 3 weeks!! They should consult a doctor regarding their results before then. 

5. Thank the participant and end the call. " ' where id = 506
update DataCollection set [Label Text] =  N'"Oh no ):

Do make sure that you try calling this participant again - at least 3 times on 3 separate occasions - before the end of Telehealth Phase II (ends 21 Jan 2018). 

Remember that the onus is now on you to make sure that the call goes through; otherwise, this participant may never go to see the doctor to review his abnormal blood test results!

In the meantime, please press ''save'' on this form, and update the ''CALL STATUS'' of this participant, to ""Did not pick up on 1st call (Phase II)"". 

You may now move on to call the next participant on your list. Thank you!" ' where id = 511
update DataCollection set [Label Text] =  N'"Oh no ):

Do make sure that you try calling this participant again - at least 3 times on 3 separate occasions - before the end of Telehealth Phase II (ends 21 Jan 2018). 

Remember that the onus is now on you to make sure that the call goes through; otherwise, this participant may never go to see the doctor to review his abnormal blood test results!

In the meantime, please press ''save'' on this form, and update the ''CALL STATUS'' of this participant, to ""Did not pick up on 2nd call (Phase II)"". 

You may now move on to call the next participant on your list. Thank you!" ' where id = 512
update DataCollection set [Label Text] =  N'"Thank you for your hard work! It is unfortunate that the participant did not pick up the call, and this could be due to a variety of reasons. Nevertheless, we really appreciate your efforts in linking up with the participant. 

Please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Did not pick up on 3rd call (Phase II)''.

Thank you! You may now move on to call another participant." ' where id = 513
update DataCollection set [Label Text] =  N'"1. Formalities - introduce yourself, remind participant that s/he attended PHS Screening Event on 21/22 Oct 2017 at Jurong East. Inform that the purpose of call is to follow-up on the screening.

2. Verify participant identity by asking for NRIC (last 4 digits)

3. Remind him that we had called him in November last year (Telehealth Phase I call), also for the purpose of follow-up. He should remember!

4. Check if he has already consulted a doctor about his screening results." ' where id = 514
update DataCollection set [Label Text] =  N'"6. Ask him about how his consultation with the doctor went. Record any main points in the textbox below. 

7. Thank the participant and end the call! Press ''Done!'' to continue." ' where id = 515
update DataCollection set [Label Text] =  N'"6. Refer him to his Screening Results and Blood Test results. You may view them at the top of this form, if necessary.
Highlight his blood pressure readings and/or some blood test values were found to lie outside the ideal range. These would have been done in the first call to this participant too.

7. As this will be our last follow-up call to this participant, encourage him to see the doctor for his abnormal screening results asap!
- This is VERY important!!
- Further encourage him by explaining the benefits of early treatment and the potential adverse consequences of not doing so.

8. Remind him that his blood test results will only be valid (as in accurate) for the next 3 weeks!! They should consult a doctor regarding their results before then.
Otherwise, he may have to take another blood test to obtain more accurate results. 

9. Thank the participant and end the call. Press ''Done!'' to continue." ' where id = 516
update DataCollection set [Label Text] =  N'"HOORAY, you''re done! 
If any, please specify any additional important remarks that you''d like us to know about your call. If you have none, please indicate NIL. 
Please press ''save'' on this form, and update the ''CALL STATUS'' of this participant to ''Telehealth Phase II completed''. " ' where id = 517




select id, [Label Text], LEN([Label Text]), textlength from DataCollection where LEN([Label Text]) < textlength order by id 

select id, [Label Text] from datacollection where [Label Text] like '%??%'

