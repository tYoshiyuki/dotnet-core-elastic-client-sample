FROM docker.elastic.co/elasticsearch/elasticsearch:7.4.2
RUN elasticsearch-plugin install analysis-kuromoji