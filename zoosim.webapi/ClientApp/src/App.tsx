import { useEffect, useState } from 'react'
import './App.css'
import TimePanel from './components/TimePanel'
import ActionsPanel from './components/ActionsPanel'
import { ZooState } from './api'
import * as api from './api'
import AnimalCard from './components/AnimalCard'

function App() {
    const [time, setTime] = useState(new Date())
    const [hoursPassed, setHoursPassed] = useState(0);

    const [monkeys, setMonkeys] = useState<api.Animal[]>([])
    const [giraffes, setGiraffes] = useState<api.Animal[]>([])
    const [elephants, setElephants] = useState<api.Animal[]>([])

    useEffect(() => {
        api.getAllAnimals()
            .then(zooState => {
                if (zooState) {
                    setMonkeys(zooState.monkeys)
                    setGiraffes(zooState.giraffes)
                    setElephants(zooState.elephants)
                }
            })
            .catch(err => {
                console.error("Error occurred when trying to get all animals", err)
            })
    }, [])

    function onSecondPassed() {
        setTime(prev => {
            let timestamp = prev.setSeconds(prev.getSeconds() + 1);
            return new Date(timestamp);
        })
    }

    function onHourPassed() {
        setHoursPassed(prev => prev + 1);

        setTime(prev => {
            let timestamp = prev.setHours(prev.getHours() + 1);
            return new Date(timestamp);
        })

        api.cycleTimeBy1Hour()
            .then(zooState => {
                refreshAnimals(zooState)
            })
            .catch(err => {
                console.error("Error occurred when trying to cycle time", err)
            })
    }

    function refreshAnimals(zooState: ZooState) {
        const { monkeys, giraffes, elephants } = zooState;
        setMonkeys(monkeys)
        setGiraffes(giraffes);
        setElephants(elephants);
    }

    function feedAnimals() {
        api.feedAllAnimals()
            .then(zooState => {
                refreshAnimals(zooState);
            })
            .catch(err => {
                console.error("Error occurred when trying to feed animals", err)
            })
    }
    function reset() {
        setHoursPassed(0);
        setTime(new Date());

        api.reviveAllAnimals()
            .then(zooState => {
                refreshAnimals(zooState);
            })
            .catch(err => {
                console.error("Error occurred when trying to revive animals", err)
            })

    }

  return (
    <>
          <header className="container">
              <h1>{"zoo sim >:]"}</h1>
          </header>
          <main className="container container-flex">
              <section className="game">
                  <div className="animal-grid">{monkeys.map(monkey => <AnimalCard animal={monkey} />)}</div>
                  <div className="animal-grid">{giraffes.map(giraffe => <AnimalCard animal={giraffe} />)}</div>
                  <div className="animal-grid">{elephants.map(elephant => <AnimalCard animal={elephant} />)}</div>
              </section>
              <aside>
                  <TimePanel time={time} hoursPassed={hoursPassed} onHourPassed={onHourPassed} onSecondPassed={onSecondPassed} />
                  <ActionsPanel onFeedButtonClick={feedAnimals} onResetButtonClick={reset} />
              </aside>
          </main>
    </>
  )
}

export default App
