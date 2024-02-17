#!/bin/bash

sudo systemctl restart etcd
sudo systemctl restart postgresql
sudo systemctl restart pgbouncer
sudo systemctl restart patroni