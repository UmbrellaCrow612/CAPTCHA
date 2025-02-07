"use client"

import { useState, useEffect } from "react"
import { Button } from "@/components/ui/button"

interface Child {
  id: string
  visualText: string
}

interface KeypadData {
  id: string
  children: Child[]
  visualAnswer: string
}

export default function KeypadGame() {
  const [data, setData] = useState<KeypadData | null>(null)
  const [clickedSequence, setClickedSequence] = useState<string[]>([])
  const [clickedIds, setClickedIds] = useState<string[]>([])
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch("https://localhost:7153/captcha/key-pad")
        if (!response.ok) {
          throw new Error("Failed to fetch data")
        }
        const jsonData = await response.json()
        setData(jsonData)
      } catch (error) {
        console.error("Error fetching data:", error)
        setError("Failed to load keypad data. Please try again later.")
      }
    }

    fetchData()
  }, [])

  const handleButtonClick = (id: string, visualText: string) => {
    setClickedSequence((prev) => [...prev, visualText])
    setClickedIds((prev) => [...prev, id])
  }

  const handleUndo = () => {
    setClickedSequence((prev) => prev.slice(0, -1))
    setClickedIds((prev) => prev.slice(0, -1))
  }

  const handleSubmit = async () => {
    if (!data) return

    if (clickedSequence.join("") === data.visualAnswer) {
      alert("Correct sequence!")
    } else {
      alert("Incorrect sequence. Try again!")
    }

    try {
      const response = await fetch("https://localhost:7153/captcha/key-pad", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          id: data.id,
          selectedChildrenId: clickedIds
        }),
      })

      if (!response.ok) {
        throw new Error("Failed to submit sequence")
      }
    } catch (error) {
      console.error("Error submitting sequence:", error)
      setError("Failed to submit sequence. Please try again.")
    }
  }

  if (error) {
    return <div className="text-center text-red-500">{error}</div>
  }

  if (!data) {
    return <div className="text-center">Loading...</div>
  }

  return (
    <div className="container mx-auto p-4 max-w-md">
      <h1 className="text-2xl font-bold mb-4 text-center">Keypad Game</h1>
      <div className="bg-gray-100 p-4 rounded-lg mb-4 text-center">
        <p className="text-lg font-semibold">Answer to match: {data.visualAnswer}</p>
      </div>
      <div className="grid grid-cols-3 gap-4 mb-4">
        {data.children.map((child) => (
          <Button key={child.id} onClick={() => handleButtonClick(child.id, child.visualText)} className="text-xl py-6">
            {child.visualText}
          </Button>
        ))}
      </div>
      <div className="bg-gray-100 p-4 rounded-lg mb-4">
        <p className="text-lg font-semibold mb-2">Your sequence:</p>
        <p className="text-xl">{clickedSequence.join(" ")}</p>
      </div>
      <div className="flex justify-between">
        <Button onClick={handleUndo} disabled={clickedSequence.length === 0}>
          Undo
        </Button>
        <Button onClick={handleSubmit}>Submit</Button>
      </div>
    </div>
  )
}