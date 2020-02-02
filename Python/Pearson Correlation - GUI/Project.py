# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'Project.ui'
#
# Created by: PyQt5 UI code generator 5.13.2
#
# WARNING! All changes made in this file will be lost!


from PyQt5 import QtCore, QtGui, QtWidgets
from PyQt5.QtWidgets import  QTableWidgetItem,QTableWidget
import numpy as np

class Ui_MainWindow(object):
    def setupUi(self, MainWindow):
        MainWindow.setObjectName("MainWindow")
        MainWindow.resize(800, 600)
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Fixed, QtWidgets.QSizePolicy.Fixed)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(MainWindow.sizePolicy().hasHeightForWidth())
        MainWindow.setSizePolicy(sizePolicy)
        self.centralwidget = QtWidgets.QWidget(MainWindow)
        self.centralwidget.setObjectName("centralwidget")


        
        users = 944
        movies = 1683


        #Table Properties
        self.tableWidget = QtWidgets.QTableWidget(self.centralwidget)
        self.tableWidget.setGeometry(QtCore.QRect(10, 0, 781, 521))
        self.tableWidget.setObjectName("tableWidget")
        self.tableWidget.setColumnCount(movies)
        self.tableWidget.setRowCount(users)


        #Rating table button
        self.btn1 = QtWidgets.QPushButton(self.centralwidget)
        self.btn1.setGeometry(QtCore.QRect(10, 530, 75, 23))
        self.btn1.setObjectName("btn1")
        self.btn1.clicked.connect(self.SetRatingData)


        #Pearson table button
        self.btn2 = QtWidgets.QPushButton(self.centralwidget)
        self.btn2.setGeometry(QtCore.QRect(90, 530, 75, 23))
        self.btn2.setObjectName("btn2")
        self.btn2.clicked.connect(self.SetPearsonData)


        MainWindow.setCentralWidget(self.centralwidget)
        self.menubar = QtWidgets.QMenuBar(MainWindow)
        self.menubar.setGeometry(QtCore.QRect(0, 0, 800, 21))
        self.menubar.setObjectName("menubar")
        MainWindow.setMenuBar(self.menubar)
        self.statusbar = QtWidgets.QStatusBar(MainWindow)
        self.statusbar.setObjectName("statusbar")
        MainWindow.setStatusBar(self.statusbar)

        self.retranslateUi(MainWindow)
        QtCore.QMetaObject.connectSlotsByName(MainWindow)

    def retranslateUi(self, MainWindow):
        _translate = QtCore.QCoreApplication.translate
        MainWindow.setWindowTitle(_translate("MainWindow", "MainWindow"))
        self.btn1.setText(_translate("MainWindow", "Rating table"))
        self.btn2.setText(_translate("MainWindow", "Pearson"))

    def SetRatingData(self, MainWindow):
        filename = open('data.txt','r')
        line = filename.readline().rstrip()
        users = 944
        movies = 1683
        RatingArray =  np.zeros((users, movies))
        #loop through the entire file and insert the records to the database
        while line !="":
            rating = line.split('\t')
            RatingArray[int(rating[0])][int(rating[1])] = int(rating[2])
            line = filename.readline().rstrip()

        headers= list()
        for i in range(0,movies):
            headers.append("M"+str(i))
        self.tableWidget.setHorizontalHeaderLabels(headers)

        for i in range(0,users):
            for j in range(0,movies):
                self.tableWidget.setItem(i,j,QTableWidgetItem(str(RatingArray[i][j])))

    
    def SetPearsonData(self, MainWindow):
        filename = open('data.txt','r')
        line = filename.readline().rstrip()
        users = 944
        movies = 1683
        RatingArray =  np.zeros((users, movies))
        #loop through the entire file and insert the records to the database
        while line !="":
            rating = line.split('\t')
            RatingArray[int(rating[0])][int(rating[1])] = int(rating[2])
            line = filename.readline().rstrip()

        x=100

        while self.tableWidget.rowCount() > 0 :
            self.tableWidget.removeRow(0)
        while self.tableWidget.columnCount() > 0:
            self.tableWidget.removeColumn(0)


        self.tableWidget.setColumnCount(x)
        self.tableWidget.setRowCount(x)

        headers= list()
        for i in range(0,x):
            headers.append("User "+str(i+1))
        self.tableWidget.setHorizontalHeaderLabels(headers)

        
        UsersCorrelation =  np.ones((x,x))
        UpperSum = 0.0
        sum_xi_xbar_pwr2 = 0.0
        sum_yi_ybar_pwr2 = 0.0

        for i in range(0,x):
            for j in range(i,x-1):
                xbar = sum(RatingArray[i][:]) / movies
                ybar = sum(RatingArray[j+1][:]) / movies

                for k in range(0,movies):
                    xi_xbar = RatingArray[i][k] - xbar
                    yi_ybar = RatingArray[j+1][k] - ybar

                    UpperSum =UpperSum + (xi_xbar) * (yi_ybar)

                    sum_xi_xbar_pwr2 = sum_xi_xbar_pwr2 + xi_xbar ** 2
                    sum_yi_ybar_pwr2 = sum_yi_ybar_pwr2 + yi_ybar ** 2
                
                lower = ( sum_xi_xbar_pwr2 * sum_yi_ybar_pwr2 ) ** 0.5

                if UpperSum ==0 or lower==0:
                    UsersCorrelation[i][j+1] = 0
                    UsersCorrelation[j + 1][i] = 0
                    UpperSum = 0
                    sum_xi_xbar_pwr2 = 0 
                    sum_yi_ybar_pwr2 = 0
                    continue

                r = UpperSum / lower
                UsersCorrelation[i][j+1] = r
                UsersCorrelation[j + 1][i] = r

                UpperSum = 0
                sum_xi_xbar_pwr2 = 0 
                sum_yi_ybar_pwr2 = 0


        for i in range(0,x):
            for j in range(0,x):
                self.tableWidget.setItem(i,j,QTableWidgetItem(str(UsersCorrelation[i][j])))