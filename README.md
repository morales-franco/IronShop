# IronShop
Basic shop &amp; product management

Features

ABM Product
1. Alta de Productos
2. Modificación de Productos
3. Eliminación de Productos
4. Grilla de Productos

Compra, buy!
1. Grilla con Fotos - Agregar al carrito (Parte Izquierda)
2. Cart - Carrito de compra (Parte derecha)
3. Confirm Order!


----------------------------------
Business Data Base

Product
productId: number;
categoryId: string;
price: number;
name: string;
description: string;

Category
categoryId: number
description: string;

Order
orderId: number;
orderDate: Date
userId: string
items: Array<OrderItem> = new Array<OrderItem>();

OrderItem
orderItemId number;
orderId
quantity: number;
unitPrice: number;
productId: number;
productCategory: string;
