url = 'https://api.au-syd.text-to-speech.watson.cloud.ibm.com/instances/4a5b475a-e502-4b7d-ba09-5dbc631663ec'
apikey = 'WbGnTbo92UyeqVF4MPh0kFMTAubABtJyeFWzqfQrQ5ue'


from ibm_watson import TextToSpeechV1
from ibm_cloud_sdk_core.authenticators import IAMAuthenticator

authenticator = IAMAuthenticator(apikey)
tts = TextToSpeechV1(authenticator=authenticator)
tts.set_service_url(url)

chinese_text = "你好,世界!"

# Specify the Mandarin Chinese voice
voice = 'zh-CN_WangWeiVoice'

with open('./speech3.mp3', 'wb') as audio_file:
    res = tts.synthesize(chinese_text, accept='audio/mp3', voice=voice).get_result()
    audio_file.write(res.content)