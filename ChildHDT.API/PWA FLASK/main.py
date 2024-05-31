from flask import Flask, request, jsonify
from flask_cors import CORS
import numpy as np
import joblib
from tensorflow.keras.models import Sequential
from tensorflow.keras.layers import Dense

app = Flask(__name__)
CORS(app, resources={r"/*": {"origins": "*"}}, supports_credentials=True)

scaler = joblib.load('scaler.pkl')

def predict(x_o, y_o, x_v, y_v, x_b, y_b, sl, intervencion):
    model = Sequential([
        Dense(64, input_dim=8, activation='relu'),
        Dense(32, activation='relu'),
        Dense(3, activation='softmax')
    ])
    model.load_weights('model_weights.h5')

    inputs = np.array([[x_o, y_o, x_v, y_v, x_b, y_b, sl, intervencion]])

    inputs = scaler.transform(inputs)
    prediction = model.predict(inputs)
    return prediction.argmax(axis=1)[0]

def normalizar(valor, min_valor, max_valor):
    return (valor - min_valor) / (max_valor - min_valor)

def calcular_estresslevel(angulo_de_vision, proximidad, velocidad,
                         min_angulo, max_angulo,
                         min_proximidad, max_proximidad,
                         min_velocidad, max_velocidad,
                         peso_angulo, peso_proximidad, peso_velocidad):

   angulo_norm = normalizar(angulo_de_vision, min_angulo, max_angulo)
   proximidad_norm = normalizar(proximidad, min_proximidad, max_proximidad)
   velocidad_norm = normalizar(velocidad, min_velocidad, max_velocidad)

   estresslevel = (
       (angulo_norm * peso_angulo) +
       (proximidad_norm * peso_proximidad) +
       (velocidad_norm * peso_velocidad)
   ) / (peso_angulo + peso_proximidad + peso_velocidad)
   return estresslevel

@app.route('/predict', methods=['POST'])
def predict_route():
    data = request.json
    print(data)
    x_o = int(data['x_o'])
    y_o = int(data['y_o'])
    x_v = int(data['x_v'])
    y_v = int(data['y_v'])
    x_b = int(data['x_b'])
    y_b = int(data['y_b'])
    sl = int(data['sl'])
    intervencion = int(data['intervencion'])
    
    prediction = predict(x_o, y_o, x_v, y_v, x_b, y_b, sl, intervencion)
    
    return jsonify({'prediction': int(prediction)}), 200

@app.route('/stresslevel', methods=['POST'])
def estresslevel_route():
    data = request.json
    angulo_de_vision = data['angulo_de_vision']
    proximidad = data['proximidad']
    velocidad = data['velocidad']
    
    estresslevel = calcular_estresslevel(angulo_de_vision, proximidad, velocidad, min_angulo=0, max_angulo=3, min_proximidad=0, max_proximidad=20, min_velocidad=0, max_velocidad=5, peso_angulo=0.7, peso_proximidad=0.2, peso_velocidad=0.3)
    
    return jsonify({'stresslevel': estresslevel}), 200

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
