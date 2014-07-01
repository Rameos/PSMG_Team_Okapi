import requests
import glob
import urllib


BASE_URL = "http://translate.google.com/translate_tts?tl=en&q=%s"


class TTSTextFile(object):

    def __init__(self, file_name, text=None):
        self.file_name = file_name
        self._cached_text = text

    @property
    def text(self):
        return self._cached_text or self._read_file_content()

    def _read_file_content(self):
        with open(self.file_name) as f:
            self._cached_text = f.read()
            return self._cached_text


def download_mp3_files(tts_text_files):
    mp3_contents = []
    for text_file in tts_text_files:
        if len(text_file.text) > 100:
            print "text in %s is too long (%s > 100)" % (text_file.file_name, len(text_file.text))

        response = requests.get(BASE_URL % text_file.text)
        if response.status_code != 200:
            pass
            #print "text in %s resulted in status code %s" % (file_name, response.status_code)

        mp3_contents.append(response.content)

    return mp3_contents


def save_mp3_files(tts_text_files, mp3_contents):
    for text_file, mp3_content in zip(tts_text_files, mp3_contents):
        save_file(text_file.file_name[:-4] + ".mp3", mp3_content)


def save_file(file_name, content):
    with open(file_name, 'wb') as f:
        f.write(content)


def read_tts_text_files():
    tts_file_names = glob.glob("*.txt")

    return [TTSTextFile(file_name) for file_name in tts_file_names]


def partition_long_files(tts_text_files):
    new_tts_text_files = []

    for text_file in tts_text_files:
        if len(text_file.text) > 100:
            partitioned_text_files = partition_file(text_file)

            new_tts_text_files += partitioned_text_files
        else:
            new_tts_text_files.append(text_file)

    return new_tts_text_files


def partition_file(text_file):
    parts = text_file.text.split(". ")
    file_names = [text_file.file_name[:-4] + "_part" + str(i) + ".txt" for i, _ in enumerate(parts)]

    return [TTSTextFile(file_name, text=text) for file_name, text in zip(file_names, parts)]


if __name__ == '__main__':
    tts_text_files = read_tts_text_files()
    tts_text_files = partition_long_files(tts_text_files)

    mp3_contents = download_mp3_files(tts_text_files)
    save_mp3_files(tts_text_files, mp3_contents)

