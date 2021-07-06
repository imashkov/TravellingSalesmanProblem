# TravellingSalesmanProblem
Программа находит решение для задачи коммивояжера:
В логистическом центре имеются заказы, каждый из которых необходимо доставить в свой пункт назначения.
По каждому из заказов задан директивный срок – время, не позже которого заказа должен быть доставлен.
Задано время, требуемое для перемещения из логистического центра в пункты назначения, и время, требуемое для перемещения между пунктами назначения.
Требуется определить такой порядок доставки заказов, при котором число заказов с нарушенным директивным сроком будет минимальным. 

Задача решена методом ветвей и границ с 2 способами ветвления: обходом в глубину и обходом в ширину с бросанием ветвления при равенстве верхней и нижней оценок.