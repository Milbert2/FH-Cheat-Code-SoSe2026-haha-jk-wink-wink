import matplotlib.pyplot as plt
import pandas as pd

df = pd.read_csv('runtimes.csv',sep=';')
plt.scatter(df.n, df.time)
plt.xlabel('Anzahl Elemente')
plt.ylabel('Zeit')
plt.savefig('Zeitplot.pdf')