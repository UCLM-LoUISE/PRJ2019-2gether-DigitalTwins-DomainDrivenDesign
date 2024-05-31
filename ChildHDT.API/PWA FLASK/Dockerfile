FROM python:3.9-slim

ENV PYTHONUNBUFFERED True
ENV APP_HOME /app
ENV PORT 5000

WORKDIR $APP_HOME

COPY . .

RUN apt-get update && apt-get install -y --no-install-recommends \
    gcc \
    && rm -rf /var/lib/apt/lists/*

RUN pip install --no-cache-dir -r requirements.txt gunicorn

CMD exec gunicorn --bind :$PORT --workers 1 --threads 8 --timeout 0 main:app
