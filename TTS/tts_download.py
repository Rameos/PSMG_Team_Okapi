import requests


BASE_URL = ""


def download_files(texts):
    responses = [requests.get(BASE_URL) for text in texts]


def read_texts():
    with open('structured_tts.txt', 'r') as f:
        text_list = [line for line in f.readlines()]

        return text_list



if __name__ == '__main__':
    ts = read_texts()

    for t in ts:
        print t
    #download_files(texts)