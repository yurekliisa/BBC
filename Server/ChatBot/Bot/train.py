import nltk
from nltk.stem.lancaster import LancasterStemmer

import numpy as np
import tensorflow as tf
import tflearn
import random
import pickle

from Bot import path
import json

stemmer = LancasterStemmer()
with open(path.getJsonPath()) as json_data:
    intents = json.load(json_data)

words = []
classes = []
documents = []
ignore_words = ['?']
for intent in intents['intents']: #Tümsözcüklerde dolaş
    for pattern in intent['patterns']:
        w = nltk.word_tokenize(pattern)   #nltk tüm patternleri tokineze edecek                                                         
        words.extend(w)
        documents.append((w, intent['tag']))                               #steming kısmı
        if intent['tag'] not in classes:
            classes.append(intent['tag'])

words = [stemmer.stem(w.lower()) for w in words if w not in ignore_words] #Burda tüm hepsini küçük harfe dönüştürdük çünkü  karşılaştırma yapıldıgında büyük küçük harf farkı olmasn
words = sorted(list(set(words))) #yineleyen kelimeleri kaldıracak sıralayacak

classes = sorted(list(set(classes))) #Tagları sıralayıp yineleyen leri kaldırack

print(len(documents), "Docs")
print(len(classes), "Classes", classes)
print(len(words), "Split words", words)

training = []
output = []
output_empty = [0] * len(classes) #sıcak kodların koyulacagı yer

for doc in documents:
    bag = []
    pattern_words = doc[0]									# burda tüm kelimelere bakacağız ana kelime torbamızda varsa 1 koyacagız kutuya yoksa 0 
    pattern_words = [stemmer.stem(word.lower()) for word in pattern_words]
    for w in words:
        bag.append(1) if w in pattern_words else bag.append(0)

    output_row = list(output_empty) 
    output_row[classes.index(doc[1])] = 1

    training.append([bag, output_row]) 

random.shuffle(training) #verileri karıştırdık
training = np.array(training)  #numpy kütühnaesi kullanılarak array oluşturduj

train_x = list(training[:, 0])      #eğitilmiş datayı sıfır bir diye iki ayrı listeye dönüştürdük        
train_y = list(training[:, 1])                                                                                                                      #kelimeleri ve  verileri modelimize girmeye hazır kıldık

tf.reset_default_graph()
net = tflearn.input_data(shape=[None, len(train_x[0])]) #
net = tflearn.fully_connected(net, 8)
net = tflearn.fully_connected(net, 8)
net = tflearn.fully_connected(net, len(train_y[0]), activation='softmax') #olanözcükler
net = tflearn.regression(net)

model = tflearn.DNN(net, tensorboard_dir=path.getPath('train_logs'))
model.fit(train_x, train_y, n_epoch=20000, batch_size=500, show_metric=True)
model.save(path.getPath('model.tflearn'))


def clean_up_sentence(sentence):       #cümleleri temizleyeceğiz 
    sentence_words = nltk.word_tokenize(sentence)
    sentence_words = [stemmer.stem(word.lower()) for word in sentence_words] #büyük küçük harf farkını ortdan kaldırdık
    return sentence_words   #cümlelerdeki kelimeleri döndürdük


def bow(sentence, words, show_details=False):
    sentence_words = clean_up_sentence(sentence)
    bag = [0] * len(words)
    for s in sentence_words:
        for i, w in enumerate(words):  #kelimenin indexinde döncek
            if w == s:
                bag[i] = 1
                if show_details:
                    print("found in bag: %s" % w)
    return np.array(bag)


pickle.dump({'words': words, 'classes': classes, 'train_x': train_x, 'train_y': train_y},
            open(path.getPath('trained_data'), "wb"))
