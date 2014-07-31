The tts_download.py script reads every .txt file in the directory and uses a Google-Voice API to convert them into a .mp3 file with the same name.

Note that existing .mp3 files will be overwritten without warning!

There is also a 100 character limitation by the API, so sentences (ending with ". ") have to be shorter than 100 characters.

To run the script you'll need Python 2.6+ and the Requests library.

For Windows these can be found here:
https://www.python.org/downloads/
http://www.lfd.uci.edu/~gohlke/pythonlibs/#requests

Navigate your terminal to the directory the tts_download.py is in and execute:

python tts_download.py