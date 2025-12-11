def sum_sequence():
    n = int(input())
    if n == 0:
        return 0
    return n + sum_sequence()

print(sum_sequence())