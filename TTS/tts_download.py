import requests
import glob


BASE_URL = "http://translate.google.com/translate_tts?tl=en&q=%s"


def download_files(texts):
    responses = [requests.get(BASE_URL % text) for text in texts]

    return [r.content for r in responses]


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


if __name__ == '__main__':
    files, texts = read_texts()

    contents = download_files(texts)

    for item in zip(files, contents):
        save_file(item[0][:-4] + ".mp3", item[1])