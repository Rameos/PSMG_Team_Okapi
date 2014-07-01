import requests
import glob
import urllib


BASE_URL = "http://translate.google.com/translate_tts?tl=en&q=%s"


def download_files(files, texts):
    contents = []
    for file_name, text in zip(files, texts):
        if len(text) > 100:
            print "text in %s is too long (%s > 100)" % (file_name, len(text))

        response = requests.get(BASE_URL % text)
        if response.status_code != 200:
            pass
            #print "text in %s resulted in status code %s" % (file_name, response.status_code)

        contents.append(response.content)

    return contents


def save_file(file_name, content):
    with open(file_name, 'wb') as f:
        f.write(content)


def read_texts():
    files = glob.glob("*.txt")

    text_list = []
    for file_name in files:
        with open(file_name, 'r') as f:
            text_list.append(f.read())

    return files, text_list


def partition_long_files(files, texts):
    new_files, new_texts = [], []

    for file_name, text in zip(files, texts):
        if len(text) > 100:
            fs, ts = partition_file(file_name, text)
            new_files += fs
            new_texts += ts

        else:
            new_files.append(file_name)
            new_texts.append(text)

    return new_files, new_texts


def partition_file(file_name, text):
    parts = text.split(". ")
    file_names = [file_name[:-4] + "_part" + str(i) + ".txt" for i, _ in enumerate(parts)]

    return file_names, parts


if __name__ == '__main__':
    files, texts = read_texts()
    files, texts = partition_long_files(files, texts)

    contents = download_files(files, texts)

    for file_name, content in zip(files, contents):
        save_file(file_name[:-4] + ".mp3", content)