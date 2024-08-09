from ibm_watson import TextToSpeechV1
# from ibm_cloud_sdk_core.authenticators import IAMAuthenticator

# authenticator = IAMAuthenticator(apikey)
# tts = TextToSpeechV1(authenticator=authenticator)
# tts.set_service_url(url)

# with open('./speech.mp3', 'wb') as audio_file:
#     res = tts.synthesize('Hello World!', accept = 'audio/mp3', voice='en-US_AllisonV3Voice').get_result()
#     audio_file.write(res.content)