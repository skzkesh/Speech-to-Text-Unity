<h1 align="left">Speech to Text Project with Unity and Hugging Face API</h1>
<h2 align="left">Introduction</h2>

**Speech-to-Text Game** is a game that challenges players to pronounce English words displayed on the screen correctly. The objective is to collect as many points as possible within a 1-minute timeframe by accurately pronouncing the words. <br>

The game features a diverse set of Unity-based components, including: <br>
  **1. Countdown Timer**: A dynamic timer that keeps track of the 1-minute gameplay session. <br>
  **2. Point Collection**: A scoring system that rewards players for correctly pronouncing the displayed words. <br>
  **3. Loudness Detection**: The game utilizes the user's microphone input to assess the volume and accuracy of their pronunciation. <br>
  **4. Data Integration**: The game reads word data from a CSV file, allowing for the inclusion of a wide range of vocabulary. <br>

The project leverages the powerful Hugging Face API for the speech-to-text mechanism, which converts the user's spoken words into text for comparison with the displayed word. Additionally, the game utilizes the Bayat Games Free Platform Game Assets from the Unity Asset Store, providing a visually appealing and polished user experience. <br>

<h2 align="left">Table of Content</h2>
<ul>
 <li a href="#guide">Project Installation and Set Up Guide</a></li>
 <li a href="#requirements">Requirements</a></li>
 <li a href="#features">Game Features</a></li>
 <li a href="#architecture">Architecture</a></li>
</ul>

<h2 id="guide">Project Installation and Set Up Guide</h2>

**Step 1**: Download <a href="https://unity.com/download">Unity Hub </a>.

**Step 2**: Choose the unity editor version 2022.3.32f1.

**Step 3**: Download ZIP of this project in Github. Extract the file on your computer.

**Step 4**: Open your Unity Hub Projects section. Click ‘add’ then choose ‘add project from disk’.

**Step 5**: Click the project name to access it.

**Step 6**: Open the “STTGame” Scene through Project → Assets → Scenes. 

**Step 7**: Click run to start the game. Follow the instruction on how to play the game.

<h2 id="requirements">Requirements</h2>

<ul>
 <li><Hugging Face API</li>
 <li>Bayat Games Free Platform Game Assets</li>
</ul>

<h3>Hugging Face API</h3>
Hugging Face API provides a large collection of pre-trained language models that can be use for various NLP tasks. Hugging Face speech-to-text API, powered by the Whisper model provides a speech recognition solution with multilingual support and flexible integration. In this project, we utilise Hugging Face API to record our voice and convert it to text. The tutorial of Hugging Face API configuration in Unity can be found in this <a href="https://youtu.be/Ngmb7l7tO0I?si=iqoD4_R4gIYp0UEg">youtube</a> or this <a href="https://huggingface.co/blog/unity-api">blog</a>.

<h3>Bayat Games Free Platform Game Assets</h3>
This is the game asset that I use to create this project. It can be find in <a href="https://assetstore.unity.com/packages/2d/environments/free-platform-game-assets-85838"> Unity Asset Store</a>

<h2 id="features">Features</h2>
<h3>CSV File Reader</h3>
To import a CSV file in Unity, you must create an Excel spreadsheet and save it in CSV (Comma-Separated Values) format. Next, import the CSV file into your Unity project's asset folder. Finally, you'll need to create a C# script to read and process the data from the CSV file. This script should be flexible enough to handle the number of columns in your data. Within the script, you can include methods to retrieve random words, sentences, and passages from the CSV data to use in your game. The CSV file reader script can be accessed by navigating to the "Assets" > "Scripts" folder in your Unity project.

<h3>Loudness Meter</h3>
The Loudness Meter is a visual tool that helps users determine the volume of their voice. It features a bar graph that serves as a reference, indicating to the user when they need to speak louder so the microphone can accurately detect their voice. This <a href="https://youtu.be/GAHMreCT4SY?si=rWgkoBjaGiFQqdvR">tutorial</a> provides a detailed guide on how to create a functional loudness scale.

<h3>Countdown Timer</h3>
The countdown timer is a function in the main script that is use to limit the game duration. Once the timer is off, the game will end and display game over screen.

<H3>Point Collection</H3>
This is a function that is triggered when we succefully compare the correct word and word said by the user


<H2 id="architecture">Architecture</H2>
This is an explain of the main C# script for this game, which name "STT". <br>

1. ```Start()``` 
is a built-in method that is called once when a script is first loaded. <br>
2. ```Update()```
is a built-in method that is called once per frame. <br>
3. ```StartRecording()```
is used to start voice recording.
4. ```StopRecording()```
is used to stop voice recording.
5. ```SendRecording()```
is trigger when the recording stop. In this function, the Hugging Face API will be call to convert voice to text.
6. ```EncodeAsWAV()```
is used to generate a WAV (Waveform Audio File Format) audio file from an array of audio samples.
7. ```RemoveLeadingSpace()```
is used to removes any leading spaces from the input string.
8. ```displayCorrectWordFromArray()```
is used to display correct word on the screen.
9. ```displayScore()```
is used to display current score.
10. `SkipButtonClick()` is used to skip current word to the next word by increasing the index and displaying the new word.
11. `CountdownCoroutine()` is a coroutine (a special type of function in Unity) that is responsible for managing a countdown timer in a game or application.
12. `UpdateTimerText()` to display the current time on the screen.
