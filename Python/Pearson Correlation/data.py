import numpy as np
#open the dataset file
filename = open('data.txt','r')
#read the first line from the file
line = filename.readline().rstrip()
#split the line based on (\t) *according to the dataset format
rating = line.split('\t')

users = 944
movies = 1683
RatingArray =  np.zeros((users, movies))
#loop through the entire file and insert the records to the database
while line !="":
    rating = line.split('\t')
    RatingArray[int(rating[0])][int(rating[1])] = int(rating[2])
    line = filename.readline().rstrip()


'''
x=users
maximum = 0.0
minimum = 0.0

UsersCorrelation =  np.ones((x,x))
UpperSum = 0.0
sum_xi_xbar_pwr2 = 0.0
sum_yi_ybar_pwr2 = 0.0


for i in range(0,x):
    for j in range(i,x-1):
        xbar = sum(RatingArray[i][:]) / movies
        ybar = sum(RatingArray[j+1][:]) / movies

        if xbar ==0.0 or ybar==0.0:
            continue

        for k in range(0,movies):
            xi_xbar = RatingArray[i][k] - xbar
            yi_ybar = RatingArray[j+1][k] - ybar
            UpperSum =UpperSum + (xi_xbar) * (yi_ybar)
            sum_xi_xbar_pwr2 = sum_xi_xbar_pwr2 + xi_xbar ** 2
            sum_yi_ybar_pwr2 = sum_yi_ybar_pwr2 + yi_ybar ** 2
        
        lower = ( sum_xi_xbar_pwr2 * sum_yi_ybar_pwr2 ) ** 0.5
        r = UpperSum / lower
        UsersCorrelation[i][j+1] = r
        UsersCorrelation[j + 1][i] = r
        
        if i%100 == 0:
            print(r,"i: ",i,"j: ",j)

        if r > maximum:
            maximum = r
        elif r < minimum:
            minimum = r

        UpperSum = 0.0
        sum_xi_xbar_pwr2 = 0.0 
        sum_yi_ybar_pwr2 = 0.0
        


#print(UsersCorrelation)

'''

from scipy import linalg
def pearsonr(x, y):
    dtype = type(1.0 + x[0] + y[0])
    xmean = x.mean(dtype=dtype)
    ymean = y.mean(dtype=dtype)
    xm = x.astype(dtype) - xmean
    ym = y.astype(dtype) - ymean
    normxm = linalg.norm(xm)
    normym = linalg.norm(ym)
    r = np.dot(xm/normxm, ym/normym)
    r = max(min(r, 1.0), -1.0)
    return r



#from scipy.stats import pearsonr
# calculate Pearson's correlation



x=users
UsersCorrelation =  np.ones((x,x))

for i in range(0,x):
    for j in range(i,x-1):
        corr= pearsonr(RatingArray[i][:], RatingArray[j+1][:])
        UsersCorrelation[i][j+1] = corr
        UsersCorrelation[j + 1][i] = corr
        print(corr)



            

