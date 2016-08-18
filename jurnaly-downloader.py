#!/usr/bin/python3.4
"""Should accept an url of the first comic book page"""
import urllib.request
import shutil
import sys
import re
import os

first_page_url = sys.argv[1]

regex_pic_url = re.compile(r' *<a href="(.*)\/(.+)\.(.+)" rel="shadowbox"><b>Увеличить</b></a>')
regex_maxpics = re.compile(r'select class="C" .*>-(\d+)-</option>.*</select>', re.DOTALL)
regex_dirtree = r'.*/([^/]+)/([^/]+)/?$'


def download_to(url, filename):
    with urllib.request.urlopen(url) as response, open(filename, 'wb') as out_file:
            shutil.copyfileobj(response, out_file)


with urllib.request.urlopen( first_page_url ) as first_page_response:
    contents = bytes.decode ( first_page_response.read()  )
    m0 = re.match ( regex_dirtree, first_page_url )
    m1 = re.search( regex_pic_url, contents)
    m2 = re.search( regex_maxpics, contents)
    if m0 and m1 and m2:
        highdir, lowdir = m0.groups()
        base, sidx, ext = m1.groups()
        idx = 1 # int(sidx)
        maxidx, = map(int, m2.groups())
        print("Base url: %s\nStart index: %d\nEnd index: %d\n" % ( base, idx, maxidx))
        newpath = highdir + "/" + lowdir
        if not os.path.exists(newpath):
                os.makedirs(newpath)

        for i in range(idx, maxidx+1):
            url = base + "/" + str(i) + "." + ext
            filepath = newpath + "/" + str(i) + "." + ext
            print( "Downloading %s to %s" % (url, filepath) )
            download_to(url, filepath )

        print("Completed")

    else:
        print("Error parsing first comic page")


