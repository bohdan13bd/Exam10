using System.Xml.Linq;

import threading
import time

class Schoolboy :
    def __init__(self, name):
        self.name = name

    def do_homework(self, subject):
        print(f"{self.name} робить домашнє завдання з {subject}")
        time.sleep(2)  # Імітуємо тривалість вирішення завдання
        print(f"{self.name} завершив домашнє завдання з {subject}")

    def watch_tv_or_play_computer(self):
        print(f"{self.name} йде дивитися телевізор або грати на комп'ютері")

class Subject :
    def __init__(self, name):
        self.name = name

class Day :
    def __init__(self, subjects_order):
        self.subjects_order = subjects_order

    def perform_daily_routine(self, schoolboy):
        for subject in self.subjects_order:
            schoolboy.do_homework(subject)
        schoolboy.watch_tv_or_play_computer()

def simulate_school_week(schoolboy):
    days_of_week = [
        Day(['Українська мова', 'Математика', 'Геометрія']),  # Понеділок
        Day(['Математика', 'Геометрія', 'Українська мова']),  # Вівторок
        Day(['Геометрія', 'Українська мова', 'Математика']),  # Середа
        Day(['Українська мова', 'Математика', 'Геометрія']),  # Четвер
        Day(['Математика', 'Геометрія', 'Українська мова']),  # П'ятниця
    ]

    for day_number, day in enumerate(days_of_week, start = 1):
        print(f"\nДень {day_number}")
        day.perform_daily_routine(schoolboy)

if __name__ == "__main__":
    schoolboy = Schoolboy("Іван")
    simulate_school_week(schoolboy)
